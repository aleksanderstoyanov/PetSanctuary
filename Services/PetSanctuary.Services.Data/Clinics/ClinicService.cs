using PetSanctuary.Data.Common.Repositories;
using PetSanctuary.Data.Models;
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

        public ICollection<Clinic> GetAllClinics()
        {
            return this.clinicsRepository.AllAsNoTracking().ToList();
        }
    }
}
