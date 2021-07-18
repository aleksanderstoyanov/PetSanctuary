using PetSanctuary.Data.Common.Repositories;
using PetSanctuary.Data.Models;
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

        public VetService(IDeletableEntityRepository<Vet> vetsRepository)
        {
            this.vetsRepository = vetsRepository;
        }

        public Vet GetVetById(string id)
        {
            return this.vetsRepository.AllAsNoTracking().Where(x => x.Id == id).FirstOrDefault();
        }

        public ICollection<Vet> GetVetsById(int clinicId)
        {
            return this.vetsRepository.AllAsNoTracking().Where(x => x.ClinicId == clinicId).ToList();
        }

        public async Task UpdateDislikes(string vetId)
        {
            var vet = this.GetVetById(vetId);
            vet.Dislikes += 1;
            this.vetsRepository.Update(vet);
            await this.vetsRepository.SaveChangesAsync();
        }

        public async Task UpdateLikes(string vetId)
        {
            var vet = this.GetVetById(vetId);
            vet.Likes += 1;
            this.vetsRepository.Update(vet);
            await this.vetsRepository.SaveChangesAsync();
        }
    }
}
