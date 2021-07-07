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
                await this.cityService.Create(cityName);
                city = this.cityService.GetCityByName(cityName);
            }

            if (address == null)
            {
                await this.addressService.Create(addressName, city.Id);
                address = this.addressService.GetAddressByName(addressName);
            }

            await this.petsRepository.AddAsync(new Pet
            {
                Name = name,
                Age = age,
                Image = image,
                CityId = city.Id,
                City = city,
                AddressId = address.Id,
                Address = address,
                OwnerId = this.userService.GetUserByName(username).Id,
                CreatedOn = DateTime.UtcNow,
                Type = (PetType)Enum.Parse(typeof(PetType), type),
                Gender = (GenderType)Enum.Parse(typeof(GenderType), gender),
                IsVaccinated = isVaccinated == "No" ? false : true,
            });
            await this.petsRepository.SaveChangesAsync();
        }

        public async Task DeletePetById(string id)
        {
            var pet = this.GetPetById(id);
            pet.IsDeleted = true;
            pet.DeletedOn = DateTime.UtcNow;
            await this.petsRepository.SaveChangesAsync();
        }

        public ICollection<Pet> GetAllCats()
        {
            return this.petsRepository.All().Where(x => x.Type.Equals(PetType.Cat) && x.IsDeleted == false).ToList();
        }

        public ICollection<Pet> GetAllDogs()
        {
            return this.petsRepository.All().Where(x => x.Type.Equals(PetType.Dog) && x.IsDeleted == false).ToList();
        }

        public ICollection<Pet> GetAllUserPets(string id)
        {
            return this.petsRepository.All().Where(x => x.OwnerId == id && x.IsDeleted == false).ToList();
        }

        public Pet GetPetById(string id)
        {
            return this.petsRepository.All().Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
