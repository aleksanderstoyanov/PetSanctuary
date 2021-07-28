using PetSanctuary.Data.Common.Repositories;
using PetSanctuary.Data.Models;
using PetSanctuary.Services.Data.Cities;
using PetSanctuary.Services.Mapping;
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

        public AddressService(IDeletableEntityRepository<Address> addressRepository)
        {
            this.addressRepository = addressRepository;
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
              .To<AddressServiceModel>()
              .FirstOrDefault(address => address.Id == id);
        }

        public AddressServiceModel GetAddressByName(string name)
        {
            return this.addressRepository
              .AllAsNoTracking()
              .To<AddressServiceModel>()
              .FirstOrDefault(address => address.Name == name);
        }
    }
}
