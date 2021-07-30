using AutoMapper;
using PetSanctuary.Data.Models;
using PetSanctuary.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetSanctuary.Services.Data.Catalogs
{
    public class CatalogServiceModel : IMapFrom<Pet>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Type { get; set; }

        public string Image { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        public string City { get; set; }

        public string CreatedOn { get; set; }

        public string IsVaccinated { get; set; }

        public string PhoneNumber { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Pet, CatalogServiceModel>()
                .ForMember(pet => pet.PhoneNumber, opt => opt.MapFrom(pet => pet.Owner.PhoneNumber))
                .ForMember(pet => pet.City, opt => opt.MapFrom(pet => pet.City.Name))
                .ForMember(pet => pet.Address, opt => opt.MapFrom(pet => pet.Address.Name))
                .ForMember(pet => pet.CreatedOn, opt => opt.MapFrom(pet => pet.CreatedOn.ToString("dddd, dd MMMM yyyy")));
        }
    }
}
