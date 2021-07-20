using PetSanctuary.Data.Common.Repositories;
using PetSanctuary.Data.Models;
using PetSanctuary.Services.Data.Cities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Services.Data.Addresses
{
    public class AddressService : IAddressService
    {
        private readonly IDeletableEntityRepository<Address> addressRepository;
        private readonly ICityService cityService;

        public AddressService(IDeletableEntityRepository<Address> addressRepository, ICityService cityService)
        {
            this.addressRepository = addressRepository;
            this.cityService = cityService;
        }

        public async Task Create(string name, int cityId)
        {
            await this.addressRepository.AddAsync(new Address
            {
                Name = name,
                CityId = cityId,
                CreatedOn = DateTime.UtcNow

            });

            await this.addressRepository.SaveChangesAsync();
        }

        public AddressServiceModel GetAddressById(int id)
        {
            return this.addressRepository
                .AllAsNoTracking()
                .Where(address => address.Id == id)
                .Select(address => new AddressServiceModel
                {
                    Id = address.Id,
                    Name = address.Name,
                    City = this.cityService.GetCityById(address.CityId).Name
                })
                .FirstOrDefault();
        }

        public AddressServiceModel GetAddressByName(string name)
        {
            return this.addressRepository
                 .AllAsNoTracking()
                 .Where(address => address.Name == name)
                 .Select(address => new AddressServiceModel
                 {
                     Id = address.Id,
                     Name = address.Name,
                     City = this.cityService.GetCityById(address.CityId).Name
                 })
                 .FirstOrDefault();
        }
    }
}
