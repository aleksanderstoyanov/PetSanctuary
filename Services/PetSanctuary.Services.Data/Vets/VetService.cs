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
        private readonly IDeletableEntityRepository<Like> likeRepository;
        private readonly IDeletableEntityRepository<Dislike> dislikeRepository;
        private readonly IMemoryCache cache;

        public VetService(
            IDeletableEntityRepository<Vet> vetsRepository,
            IDeletableEntityRepository<Like> likeRepository,
            IDeletableEntityRepository<Dislike> dislikeRepository,
            IMemoryCache cache)
        {
            this.vetsRepository = vetsRepository;
            this.likeRepository = likeRepository;
            this.dislikeRepository = dislikeRepository;
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
                      .SetAbsoluteExpiration(TimeSpan.FromMinutes(30)));
            }

            return vets;
        }

        public async Task UpdateDislikesAsync(string vetId, string userId)
        {
            var vet = this.vetsRepository
                .All()
                .FirstOrDefault(vet => vet.Id == vetId);

            var dislike = this.dislikeRepository
                .All()
                .FirstOrDefault(like => like.UserId == userId);

            if (dislike != null)
            {
                this.dislikeRepository
                .Delete(dislike);
            }
            else
            {
                vet.Dislikes.Add(new Dislike
                {
                    UserId = userId,
                });
            }

            await this.dislikeRepository.SaveChangesAsync();
            await this.vetsRepository.SaveChangesAsync();
        }

        public async Task UpdateLikesAsync(string vetId, string userId)
        {
            var vet = this.vetsRepository
               .All()
               .FirstOrDefault(vet => vet.Id == vetId);

            var like = this.likeRepository
                .All()
                .FirstOrDefault(like => like.UserId == userId);

            if (like != null)
            {
                this.likeRepository
                .Delete(like);
            }
            else
            {
                vet.Likes.Add(new Like
                {
                    UserId = userId,
                });
            }

            await this.likeRepository.SaveChangesAsync();
            await this.vetsRepository.SaveChangesAsync();
        }
    }
}
