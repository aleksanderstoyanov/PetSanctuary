namespace PetSanctuary.Services.Data.Clinics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Extensions.Caching.Memory;
    using PetSanctuary.Data.Common.Repositories;
    using PetSanctuary.Data.Models;
    using PetSanctuary.Services.Mapping;

    public class ClinicService : IClinicService
    {
        private readonly IDeletableEntityRepository<Clinic> clinicsRepository;
        private readonly IMemoryCache cache;

        public ClinicService(
            IDeletableEntityRepository<Clinic> clinicsRepository,
            IMemoryCache cache)
        {
            this.clinicsRepository = clinicsRepository;
            this.cache = cache;
        }

        public IEnumerable<ClinicServiceModel> GetAllClinics()
        {
            return this.clinicsRepository
              .AllAsNoTracking()
              .To<ClinicServiceModel>()
              .ToList();
        }

        public IEnumerable<ClinicServiceModel> GetAllClinicsByCity(string city, int currentPage, int postsPerPage)
        {
            string clinicsByCityKey = $"Clinics{city}Key{currentPage}";

            var clinics = this.cache.Get<List<ClinicServiceModel>>(clinicsByCityKey);

            if (clinics == null)
            {
                if (city == "All")
                {
                    clinics = this.clinicsRepository
                        .AllAsNoTracking()
                        .Skip((currentPage - 1) * postsPerPage)
                        .Take(postsPerPage)
                        .To<ClinicServiceModel>()
                        .ToList();
                }
                else
                {
                    clinics = this.clinicsRepository
                   .AllAsNoTracking()
                   .Where(clinic => clinic.City.Name == city)
                   .Skip((currentPage - 1) * postsPerPage)
                   .Take(postsPerPage)
                   .To<ClinicServiceModel>()
                   .ToList();
                }

                this.cache.Set(clinicsByCityKey, clinics, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(30)));
            }

            return clinics;
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
