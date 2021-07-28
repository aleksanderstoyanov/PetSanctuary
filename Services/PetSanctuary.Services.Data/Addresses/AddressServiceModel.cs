using AutoMapper;
using PetSanctuary.Data.Models;
using PetSanctuary.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetSanctuary.Services.Data.Addresses
{
    public class AddressServiceModel : IMapFrom<Address>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Address, AddressServiceModel>()
                 .ForMember(address => address.City, opt => opt.MapFrom(address => address.City.Name));
        }
    }
}
