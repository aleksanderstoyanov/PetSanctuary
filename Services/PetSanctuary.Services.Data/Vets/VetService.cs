using PetSanctuary.Data.Common.Repositories;
using PetSanctuary.Data.Models;
using PetSanctuary.Services.Data.Clinics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Services.Data.Vets
{
    public class VetService : IVetService
    {
        private readonly IDeletableEntityRepository<Vet> vetsRepository;
        private readonly IClinicService clinicService;

        public VetService(IDeletableEntityRepository<Vet> vetsRepository, IClinicService clinicService)
        {
            this.vetsRepository = vetsRepository;
            this.clinicService = clinicService;
        }

        public VetServiceModel GetVetById(string id)
        {
            return this.MapVets(this.vetsRepository.AllAsNoTracking().Where(vet => vet.Id == id))
                .FirstOrDefault();
        }

        public IEnumerable<VetServiceModel> GetVetsById(int clinicId)
        {
            return this.MapVets(this.vetsRepository.AllAsNoTracking().Where(vet => vet.ClinicId == clinicId));

        }

        public async Task UpdateDislikes(string vetId)
        {
            var vet = this.vetsRepository
                .All()
                .FirstOrDefault(vet => vet.Id == vetId);

            vet.Dislikes += 1;
            this.vetsRepository.Update(vet);
            await this.vetsRepository.SaveChangesAsync();
        }

        public async Task UpdateLikes(string vetId)
        {
            var vet = this.vetsRepository
               .All()
               .FirstOrDefault(vet => vet.Id == vetId);

            vet.Likes += 1;
            this.vetsRepository.Update(vet);
            await this.vetsRepository.SaveChangesAsync();
        }

        private IEnumerable<VetServiceModel> MapVets(IQueryable<Vet> vets)
        {
            return vets
                .Select(vet => new VetServiceModel
                {
                    Id = vet.Id,
                    FirstName = vet.FirstName,
                    Surname = vet.Surname,
                    Clinic = this.clinicService.GetClinicById(vet.ClinicId).Name,
                    ClinicId = vet.ClinicId,
                    Description = vet.Description,
                    Likes = vet.Likes,
                    Dislikes = vet.Dislikes
                }).ToList();

        }
    }
}
