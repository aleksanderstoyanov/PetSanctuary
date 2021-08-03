namespace PetSanctuary.Services.Data.Clinics
{
    using System.Collections.Generic;
    using System.Linq;

    using PetSanctuary.Data.Common.Repositories;
    using PetSanctuary.Data.Models;
    using PetSanctuary.Services.Mapping;

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

        public IEnumerable<ClinicServiceModel> GetAllClinicsByCity(string city, int currentPage, int postsPerPage)
        {
            if (city == "All")
            {
                return this.clinicsRepository
                    .AllAsNoTracking()
                    .Skip((currentPage - 1) * postsPerPage)
                    .Take(postsPerPage)
                    .To<ClinicServiceModel>()
                    .ToList();
            }

            return this.clinicsRepository
             .AllAsNoTracking()
             .Where(clinic => clinic.City.Name == city)
             .Skip((currentPage - 1) * postsPerPage)
             .Take(postsPerPage)
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
