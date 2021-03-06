namespace PetSanctuary.Services.Data.Counts
{
    using System;
    using System.Linq;

    using PetSanctuary.Data.Common.Repositories;
    using PetSanctuary.Data.Models;
    using PetSanctuary.Data.Models.Enums;

    public class CountService : ICountService
    {
        private readonly IDeletableEntityRepository<Pet> catalogRepository;
        private readonly IDeletableEntityRepository<Blog> blogRepository;
        private readonly IDeletableEntityRepository<Clinic> clinicRepository;
        private readonly IRepository<BlogComment> blogCommentRepository;
        private readonly IRepository<VetComment> vetCommentRepository;
        private readonly IDeletableEntityRepository<Pet> petRepository;

        public CountService(
            IDeletableEntityRepository<Pet> catalogRepository,
            IDeletableEntityRepository<Blog> blogRepository,
            IDeletableEntityRepository<Clinic> clinicRepository,
            IRepository<BlogComment> blogCommentRepository,
            IRepository<VetComment> vetCommentRepository,
            IDeletableEntityRepository<Pet> petRepository)
        {
            this.catalogRepository = catalogRepository;
            this.blogRepository = blogRepository;
            this.clinicRepository = clinicRepository;
            this.blogCommentRepository = blogCommentRepository;
            this.vetCommentRepository = vetCommentRepository;
            this.petRepository = petRepository;
        }

        public int GetBlogCommentsCount(string id)
        {
            return this.blogCommentRepository
                .AllAsNoTracking()
                .Where(blog => blog.BlogId == id && !blog.Comment.IsDeleted)
                .Count();
        }

        public int GetVetCommentsCount(string id)
        {
            return this.vetCommentRepository
                .AllAsNoTracking()
                .Where(vet => vet.VetId == id && !vet.Comment.IsDeleted)
                .Count();
        }

        public int GetUserBlogsCount(string id, bool isAdmin)
        {
            if (isAdmin)
            {
                return this.blogRepository
                    .AllAsNoTracking()
                    .Count();
            }

            return this.blogRepository
                .AllAsNoTracking()
                .Where(blog => blog.AuthorId == id)
                .Count();
        }

        public int GetUserPostsCount(string id, bool isAdmin)
        {
            if (isAdmin)
            {
                return this.catalogRepository
                    .AllAsNoTracking()
                    .Count();
            }

            return this.catalogRepository
                .AllAsNoTracking()
                .Where(catalog => catalog.OwnerId == id)
                .Count();
        }

        public int GetTotalClinicsCountByCity(string city)
        {
            if (city == "All")
            {
                return this.clinicRepository
                 .AllAsNoTracking()
                 .Count();
            }

            return this.clinicRepository
                .AllAsNoTracking()
                .Where(clinic => clinic.City.Name == city)
                .Count();
        }

        public int GetTotalPetsByType(string type)
        {
            var petType = Enum.Parse(typeof(PetType), type);

            return this.petRepository
                .AllAsNoTracking()
                .Where(pet => pet.Type.Equals(petType))
                .Count();
        }

        public int GetAllBlogsCount()
        {
            return this.blogRepository
                 .AllAsNoTracking()
                 .Count();
        }
    }
}
