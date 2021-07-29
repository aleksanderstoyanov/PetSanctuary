using PetSanctuary.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Services.Data.Addresses
{
    public interface IAddressService
    {
        AddressServiceModel GetAddressByName(string name);

        AddressServiceModel GetAddressById(int id);

        Task CreateAsync(string name, int cityId);
    }
}
