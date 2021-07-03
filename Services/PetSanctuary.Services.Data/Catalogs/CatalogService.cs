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

        public async Task Create(string name, int age, string image, string type, string cityName, string addressName, string isVaccinated)
        {
            var city = this.cityService.GetCityByName(cityName);
            var address = this.addressService.GetAddressByName(cityName);
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
                AddressId = address.Id,
                CreatedOn = DateTime.UtcNow,
                Type = (PetType)Enum.Parse(typeof(PetType), type),
                IsVaccinated = isVaccinated == "No" ? false : true,
            });
            await this.petsRepository.SaveChangesAsync();
        }

        public ICollection<Pet> GetAllCats()
        {
            return this.petsRepository.All().Where(x => x.Type.Equals(PetType.Cat)).ToList();
        }

        public ICollection<Pet> GetAllDogs()
        {
            return this.petsRepository.All().Where(x => x.Type.Equals(PetType.Dog)).ToList();
        }
    }
}
