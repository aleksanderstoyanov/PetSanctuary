using PetSanctuary.Data.Common.Repositories;
using PetSanctuary.Data.Models;
using PetSanctuary.Services.Data.Addresses;
using PetSanctuary.Services.Data.Cities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetSanctuary.Services.Data.Clinics
{
    public class ClinicService : IClinicService
    {
        private readonly IDeletableEntityRepository<Clinic> clinicsRepository;
        private readonly IAddressService addressService;
        private readonly ICityService cityService;

        public ClinicService(IDeletableEntityRepository<Clinic> clinicsRepository, IAddressService addressService, ICityService cityService)
        {
            this.clinicsRepository = clinicsRepository;
            this.addressService = addressService;
            this.cityService = cityService;
        }

        public IEnumerable<ClinicServiceModel> GetAllClinics()
        {
            return this.clinicsRepository
                .AllAsNoTracking()
                .Select(clinic => new ClinicServiceModel
                {
                    Id = clinic.Id,
                    Name = clinic.Name,
                    Image = clinic.Image,
                    Address = this.addressService.GetAddressById(clinic.AddressId).Name,
                    City = this.cityService.GetCityById(clinic.CityId).Name
                })
                .ToList();
        }

        public ClinicServiceModel GetClinicByName(string name)
        {
            return this.clinicsRepository
                .All()
                .Select(clinic => new ClinicServiceModel
                {
                    Id = clinic.Id,
                    Name = clinic.Name,
                    Image = clinic.Image,
                    Address = this.addressService.GetAddressById(clinic.AddressId).Name,
                    City = this.cityService.GetCityById(clinic.CityId).Name
                })
                .FirstOrDefault(clinic => clinic.Name == name);
        }
    }
}
