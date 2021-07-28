using AutoMapper;
using PetSanctuary.Data.Models;
using PetSanctuary.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetSanctuary.Services.Data.Clinics
{
    public class ClinicServiceModel : IMapFrom<Clinic>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Clinic, ClinicServiceModel>()
                 .ForMember(clinic => clinic.City, opt => opt.MapFrom(clinic => clinic.City.Name))
                 .ForMember(clinic => clinic.Address, opt => opt.MapFrom(clinic => clinic.Address.Name));
        }
    }
}
