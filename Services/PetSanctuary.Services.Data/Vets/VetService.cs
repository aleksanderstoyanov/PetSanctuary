namespace PetSanctuary.Services.Data.Vets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Caching.Memory;
    using PetSanctuary.Data.Common.Repositories;
    using PetSanctuary.Data.Models;
    using PetSanctuary.Services.Mapping;

    public class VetService : IVetService
    {
        private readonly IDeletableEntityRepository<Vet> vetsRepository;
        private readonly IMemoryCache cache;

        public VetService(IDeletableEntityRepository<Vet> vetsRepository, IMemoryCache cache)
        {
            this.vetsRepository = vetsRepository;
            this.cache = cache;
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
            string memoryKey = $"VetsKey{clinicId}";
            var vets = this.cache.Get<List<VetServiceModel>>(memoryKey);
            if (vets == null)
            {
                vets = this.vetsRepository
                 .AllAsNoTracking()
                 .Where(vet => vet.ClinicId == clinicId)
                 .To<VetServiceModel>()
                 .ToList();
                this.cache.Set(memoryKey, vets, new MemoryCacheEntryOptions()
                      .SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
            }

            return vets;
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
