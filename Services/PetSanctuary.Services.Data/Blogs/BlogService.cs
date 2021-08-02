﻿namespace PetSanctuary.Services.Data.Blogs
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
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

        public async Task CreateAsync(string title, IFormFile image, string description, string authorId, string rootPath)
        {
            await this.blogRepository.AddAsync(new Blog
            {
                Title = title,
                Image = this.UploadFile(image, rootPath),
                Description = description,
                AuthorId = authorId,
                CreatedOn = DateTime.UtcNow,

            });
            await this.blogRepository.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id, string rootPath)
        {
            var blog = this.blogRepository.All().FirstOrDefault(blog => blog.Id == id);
            EnsureFileDeleted(rootPath, blog.Image);
            this.blogRepository.Delete(blog);
            await this.blogRepository.SaveChangesAsync();
        }

        public async Task EditByIdAsync(string id, string title, IFormFile image, string description, string rootPath)
        {
            var blog = this.blogRepository.All().FirstOrDefault(blog => blog.Id == id);
            blog.Title = title;
            if (image != null)
            {
                EnsureFileDeleted(rootPath, blog.Image);
                blog.Image = this.UploadFile(image, rootPath);
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

        private static void EnsureFileDeleted(string rootPath, string imageName)
        {
            string uploadDir = Path.Combine(rootPath, "img");
            var fileName = Path.Combine(uploadDir, imageName);
            var file = new FileInfo(fileName);
            if (file.Exists)
            {
                file.Delete();
            }
        }

        private string UploadFile(IFormFile image, string rootPath)
        {
            string fileName = null;
            if (image != null)
            {
                string uploadDir = Path.Combine(rootPath, "img");
                fileName = Guid.NewGuid().ToString() + "-" + image.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }

            return fileName;
        }
    }
}
