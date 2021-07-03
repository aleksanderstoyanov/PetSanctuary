using PetSanctuary.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Services.Data.Addresses
{
    public interface IAddressService
    {
        Address GetAddressByName(string name);

        Task Create(string name, int cityId);
    }
}
