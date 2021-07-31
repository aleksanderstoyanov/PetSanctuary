using PetSanctuary.Data.Common.Repositories;
using PetSanctuary.Data.Models;
using PetSanctuary.Data.Models.Enums;
using PetSanctuary.Services.Data.Addresses;
using PetSanctuary.Services.Data.Cities;
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

        public CatalogService(IDeletableEntityRepository<Pet> petsRepository, ICityService cityService, IAddressService addressService)
        {
            this.petsRepository = petsRepository;
            this.cityService = cityService;
            this.addressService = addressService;
        }

        public async Task Create(string name, int age, string image, string type, string gender, string cityName, string addressName, string isVaccinated, string ownerId)
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
                OwnerId = ownerId,
                CreatedOn = DateTime.UtcNow,
                Type = (PetType)Enum.Parse(typeof(PetType), type),
                Gender = (GenderType)Enum.Parse(typeof(GenderType), gender),
                IsVaccinated = isVaccinated == "No" ? false : true,
            };

            pet.UserPets.Add(new UserPet
            {
                UserId = ownerId,
                PetId = pet.Id
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
            return this.petsRepository
              .AllAsNoTracking()
              .Where(pet => pet.Type.Equals(PetType.Cat))
              .To<CatalogServiceModel>()
              .ToList();
        }

        public IEnumerable<CatalogServiceModel> GetAllDogs()
        {
            return this.petsRepository
              .AllAsNoTracking()
              .Where(pet => pet.Type.Equals(PetType.Dog))
              .To<CatalogServiceModel>()
              .ToList();
        }

        public IEnumerable<CatalogServiceModel> GetAllOthers()
        {
            return this.petsRepository
              .AllAsNoTracking()
              .Where(pet => pet.Type.Equals(PetType.Other))
              .To<CatalogServiceModel>()
              .ToList();
        }

        public IEnumerable<CatalogServiceModel> GetAllPets()
        {
            return this.petsRepository
              .AllAsNoTracking()
              .To<CatalogServiceModel>()
              .ToList();
        }

        public IEnumerable<CatalogServiceModel> GetAllUserPets(string id, int currentPage, int postsPerPage, bool isAdmin)
        {
            if (isAdmin)
            {
                return this.petsRepository
                    .AllAsNoTracking()
                    .Skip((currentPage - 1) * postsPerPage)
                    .Take(postsPerPage)
                    .To<CatalogServiceModel>()
                    .ToList();
            }

            return this.petsRepository
              .AllAsNoTracking()
              .Where(pet => pet.OwnerId == id)
              .Skip((currentPage - 1) * postsPerPage)
              .Take(postsPerPage)
              .To<CatalogServiceModel>()
              .ToList();
        }

        public CatalogServiceModel GetPetById(string id)
        {
            return this.petsRepository
              .All()
              .Where(pet => pet.Id == id)
              .To<CatalogServiceModel>()
              .FirstOrDefault();
        }

        private async Task<AddressServiceModel> EnsureAddressCreated(string addressName, int cityId)
        {
            await this.addressService.CreateAsync(addressName, cityId);
            return this.addressService.GetAddressByName(addressName);
        }

        private async Task<CityServiceModel> EnsureCityCreated(string cityName)
        {
            await this.cityService.CreateAsync(cityName);
            return this.cityService.GetCityByName(cityName);
        }
    }
}
