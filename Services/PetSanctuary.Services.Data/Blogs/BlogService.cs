namespace PetSanctuary.Services.Data.Blogs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using PetSanctuary.Data.Common.Repositories;
    using PetSanctuary.Data.Models;
    using PetSanctuary.Services.Data.Files;
    using PetSanctuary.Services.Mapping;

    public class BlogService : IBlogService
    {
        private readonly IDeletableEntityRepository<Blog> blogRepository;
        private readonly IFileService fileService;

        public BlogService(
            IDeletableEntityRepository<Blog> blogRepository,
            IFileService fileService)
        {
            this.blogRepository = blogRepository;
            this.fileService = fileService;
        }

        public async Task CreateAsync(string title, IFormFile image, string description, string authorId, string rootPath)
        {
            await this.blogRepository.AddAsync(new Blog
            {
                Title = title,
                Image = this.fileService.UploadFile(image, rootPath),
                Description = description,
                AuthorId = authorId,
                CreatedOn = DateTime.UtcNow,

            });
            await this.blogRepository.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id, string rootPath)
        {
            var blog = this.blogRepository.All().FirstOrDefault(blog => blog.Id == id);
            this.fileService.DeleteFile(rootPath, blog.Image);
            this.blogRepository.Delete(blog);
            await this.blogRepository.SaveChangesAsync();
        }

        public async Task EditByIdAsync(string id, string title, IFormFile image, string description, string rootPath)
        {
            var blog = this.blogRepository.All().FirstOrDefault(blog => blog.Id == id);
            blog.Title = title;
            if (image != null)
            {
                this.fileService.DeleteFile(rootPath, blog.Image);
                blog.Image = this.fileService.UploadFile(image, rootPath);
            }

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
