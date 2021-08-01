namespace PetSanctuary.Services.Data.Vets
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PetSanctuary.Data.Common.Repositories;
    using PetSanctuary.Data.Models;
    using PetSanctuary.Services.Mapping;

    public class VetService : IVetService
    {
        private readonly IDeletableEntityRepository<Vet> vetsRepository;

        public VetService(IDeletableEntityRepository<Vet> vetsRepository)
        {
            this.vetsRepository = vetsRepository;
        }

        public VetServiceModel GetVetById(string id)
        {
            return this.vetsRepository
              .AllAsNoTracking()
              .Where(vet => vet.Id == id)
              .To<VetServiceModel>()
              .FirstOrDefault();
        }

        public IEnumerable<VetServiceModel> GetVetsById(int clinicId)
        {
            return this.vetsRepository
              .AllAsNoTracking()
              .Where(vet => vet.ClinicId == clinicId)
              .To<VetServiceModel>()
              .ToList();
        }

        public async Task UpdateDislikesAsync(string vetId)
        {
            var vet = this.vetsRepository
                .All()
                .FirstOrDefault(vet => vet.Id == vetId);

            vet.Dislikes += 1;
            this.vetsRepository.Update(vet);
            await this.vetsRepository.SaveChangesAsync();
        }

        public async Task UpdateLikesAsync(string vetId)
        {
            var vet = this.vetsRepository
               .All()
               .FirstOrDefault(vet => vet.Id == vetId);

            vet.Likes += 1;
            this.vetsRepository.Update(vet);
            await this.vetsRepository.SaveChangesAsync();
        }
    }
}
