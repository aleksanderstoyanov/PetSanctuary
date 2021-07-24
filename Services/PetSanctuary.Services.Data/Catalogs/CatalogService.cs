using PetSanctuary.Data.Common.Repositories;
using PetSanctuary.Data.Models;
using PetSanctuary.Data.Models.Enums;
using PetSanctuary.Services.Data.Addresses;
using PetSanctuary.Services.Data.Cities;
using PetSanctuary.Services.Data.Users;
using PetSanctuary.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Services.Data.Catalogs
{
    public class CatalogService : ICatalogService
    {
        private readonly IDeletableEntityRepository<Pet> petsRepository;
        private readonly ICityService cityService;
        private readonly IAddressService addressService;
        private readonly IUserService userService;

        public CatalogService(IDeletableEntityRepository<Pet> petsRepository, ICityService cityService, IAddressService addressService, IUserService userService)
        {
            this.petsRepository = petsRepository;
            this.cityService = cityService;
            this.addressService = addressService;
            this.userService = userService;
        }

        public async Task Create(string name, int age, string image, string type, string gender, string cityName, string addressName, string isVaccinated, string username)
        {
            var city = this.cityService.GetCityByName(cityName);
            var address = this.addressService.GetAddressByName(addressName);
            if (city == null)
            {
                city = await this.EnsureCityCreated(cityName);
            }

            if (address == null)
            {
                address = await this.EnsureAddressCreated(addressName, city.Id);
            }

            var pet = new Pet
            {
                Name = name,
                Age = age,
                Image = image,
                CityId = city.Id,
                AddressId = address.Id,
                OwnerId = this.userService.GetUserByName(username).Id,
                CreatedOn = DateTime.UtcNow,
                Type = (PetType)Enum.Parse(typeof(PetType), type),
                Gender = (GenderType)Enum.Parse(typeof(GenderType), gender),
                IsVaccinated = isVaccinated == "No" ? false : true,
            };

            pet.UserPets.Add(new UserPet
            {
                User = this.userService.GetUserByName(username),
                Pet = pet
            });
            await this.petsRepository.AddAsync(pet);

            await this.petsRepository.SaveChangesAsync();
        }

        public async Task DeletePetById(string id)
        {
            var pet = this.petsRepository.All().FirstOrDefault(pet => pet.Id == id);
            this.petsRepository.Delete(pet);
            await this.petsRepository.SaveChangesAsync();
        }

        public async Task EditPetById(string id, string name, int age, string image, string type, string gender, string isVaccinated, string cityName, string addressName)
        {
            var city = this.cityService.GetCityByName(cityName);
            var address = this.addressService.GetAddressByName(addressName);
            if (city == null)
            {
                city = await this.EnsureCityCreated(cityName);
            }

            if (address == null)
            {
                address = await this.EnsureAddressCreated(addressName, city.Id);
            }

            var pet = this.petsRepository.All().FirstOrDefault(pet => pet.Id == id);
            pet.ModifiedOn = DateTime.UtcNow;
            pet.Name = name;
            pet.Age = age;
            pet.Image = image;
            pet.Type = (PetType)Enum.Parse(typeof(PetType), type);
            pet.Gender = (GenderType)Enum.Parse(typeof(GenderType), gender);
            pet.IsVaccinated = isVaccinated == "No" ? false : true;
            pet.CityId = city.Id;
            pet.AddressId = address.Id;

            await this.petsRepository.SaveChangesAsync();
        }

        public IEnumerable<CatalogServiceModel> GetAllCats()
        {
            return this.MapPets(this.petsRepository.AllAsNoTracking()
              .Where(pet => pet.Type.Equals(PetType.Cat)));
        }

        public IEnumerable<CatalogServiceModel> GetAllDogs()
        {
            return this.MapPets(this.petsRepository.AllAsNoTracking()
              .Where(pet => pet.Type.Equals(PetType.Dog)));
        }

        public IEnumerable<CatalogServiceModel> GetAllOthers()
        {
            return this.MapPets(this.petsRepository.AllAsNoTracking()
             .Where(pet => pet.Type.Equals(PetType.Other)));
        }

        public IEnumerable<CatalogServiceModel> GetAllPets()
        {
            return this.MapPets(this.petsRepository.AllAsNoTracking());
        }

        public IEnumerable<CatalogServiceModel> GetAllUserPets(string id)
        {
            return this.MapPets(this.petsRepository.AllAsNoTracking()
                 .Where(pet => pet.OwnerId == id));
        }

        public CatalogServiceModel GetPetById(string id)
        {
            return this.MapPets(this.petsRepository.All()
              .Where(pet => pet.Id == id))
              .FirstOrDefault();
        }

        private IEnumerable<CatalogServiceModel> MapPets(IQueryable<Pet> pets)
        {
            return pets
                .Select(pet => new CatalogServiceModel
                {
                    Id = pet.Id,
                    Name = pet.Name,
                    Age = pet.Age,
                    Image = pet.Image,
                    Address = this.addressService.GetAddressById(pet.AddressId).Name,
                    Gender = pet.Gender.ToString(),
                    City = this.cityService.GetCityById(pet.CityId).Name,
                    CreatedOn = pet.CreatedOn.ToString("ddd d MMM"),
                    IsVaccinated = pet.IsVaccinated ? "Yes" : "No",
                    Type = pet.Type.ToString(),
                    PhoneNumber = this.userService.GetUserById(pet.OwnerId).PhoneNumber,
                }).ToList();
        }

        private async Task<AddressServiceModel> EnsureAddressCreated(string addressName, int cityId)
        {
            await this.addressService.Create(addressName, cityId);
            return this.addressService.GetAddressByName(addressName);
        }

        private async Task<CityServiceModel> EnsureCityCreated(string cityName)
        {
            await this.cityService.Create(cityName);
            return this.cityService.GetCityByName(cityName);
        }
    }
}
