namespace PetSanctuary.Services.Data.Blogs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PetSanctuary.Data.Common.Repositories;
    using PetSanctuary.Data.Models;
    using PetSanctuary.Services.Mapping;

    public class BlogService : IBlogService
    {
        private readonly IDeletableEntityRepository<Blog> blogRepository;

        public BlogService(IDeletableEntityRepository<Blog> blogRepository)
        {
            this.blogRepository = blogRepository;
        }

        public async Task CreateAsync(string title, string image, string description, string authorId)
        {
            await this.blogRepository.AddAsync(new Blog
            {
                Title = title,
                Image = image,
                Description = description,
                AuthorId = authorId,
                CreatedOn = DateTime.UtcNow,

            });
            await this.blogRepository.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id)
        {
            var blog = this.blogRepository.All().FirstOrDefault(blog => blog.Id == id);
            this.blogRepository.Delete(blog);
            await this.blogRepository.SaveChangesAsync();
        }

        public async Task EditByIdAsync(string id, string title, string image, string description)
        {
            var blog = this.blogRepository.All().FirstOrDefault(blog => blog.Id == id);
            blog.Title = title;
            blog.Image = image;
            blog.Description = description;

            await this.blogRepository.SaveChangesAsync();
        }

        public IEnumerable<BlogServiceModel> GetAllBlogs()
        {
            return this.blogRepository
                .AllAsNoTracking()
                .To<BlogServiceModel>()
                .ToList();
        }

        public IEnumerable<BlogServiceModel> GetAllUserBlogs(string id, int currentPage, int postsPerPage, bool isAdmin)
        {
            if (isAdmin)
            {
                return this.blogRepository
                  .AllAsNoTracking()
                  .Skip((currentPage - 1) * postsPerPage)
                  .Take(postsPerPage)
                  .To<BlogServiceModel>()
                  .ToList();
            }

            return this.blogRepository
              .AllAsNoTracking()
              .Where(blog => blog.AuthorId == id)
              .Skip((currentPage - 1) * postsPerPage)
              .Take(postsPerPage)
              .To<BlogServiceModel>()
              .ToList();
        }

        public BlogServiceModel GetBlogById(string id)
        {
            return this.blogRepository
                .AllAsNoTracking()
                .Where(blog => blog.Id == id)
                .To<BlogServiceModel>()
                .FirstOrDefault(blog => blog.Id == id);
        }

        public BlogServiceModel GetBlogByTitle(string title)
        {
            return this.blogRepository.All()
                .To<BlogServiceModel>()
                .FirstOrDefault(blog => blog.Title == title);
        }
    }
}
