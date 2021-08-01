namespace PetSanctuary.Services.Data.Addresses
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using PetSanctuary.Data.Common.Repositories;
    using PetSanctuary.Data.Models;
    using PetSanctuary.Services.Mapping;

    public class AddressService : IAddressService
    {
        private readonly IDeletableEntityRepository<Address> addressRepository;

        public AddressService(IDeletableEntityRepository<Address> addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        public async Task CreateAsync(string name, int cityId)
        {
            await this.addressRepository.AddAsync(new Address
            {
                Name = name,
                CityId = cityId,
                CreatedOn = DateTime.UtcNow,

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
