using PetSanctuary.Data.Common.Repositories;
using PetSanctuary.Data.Models;
using PetSanctuary.Services.Data.Addresses;
using PetSanctuary.Services.Data.Cities;
using PetSanctuary.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetSanctuary.Services.Data.Clinics
{
    public class ClinicService : IClinicService
    {
        private readonly IDeletableEntityRepository<Clinic> clinicsRepository;

        public ClinicService(IDeletableEntityRepository<Clinic> clinicsRepository)
        {
            this.clinicsRepository = clinicsRepository;
        }

        public IEnumerable<ClinicServiceModel> GetAllClinics()
        {
            return this.clinicsRepository
              .AllAsNoTracking()
              .To<ClinicServiceModel>()
              .ToList();
        }

        public ClinicServiceModel GetClinicById(int id)
        {
            return this.clinicsRepository
              .All()
              .To<ClinicServiceModel>()
              .FirstOrDefault(clinic => clinic.Id == id);
        }

        public ClinicServiceModel GetClinicByName(string name)
        {
            return this.clinicsRepository
              .All()
              .To<ClinicServiceModel>()
              .FirstOrDefault(clinic => clinic.Name == name);
        }
    }
}
