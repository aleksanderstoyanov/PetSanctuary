using PetSanctuary.Data.Common.Repositories;
using PetSanctuary.Data.Models;
using PetSanctuary.Services.Data.Comments;
using PetSanctuary.Services.Data.Users;
using PetSanctuary.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Services.Data.Blogs
{
    public class BlogService : IBlogService
    {
        private readonly IDeletableEntityRepository<Blog> blogRepository;
        private readonly IUserService userService;

        public BlogService(IDeletableEntityRepository<Blog> blogRepository, IUserService userService)
        {
            this.blogRepository = blogRepository;
            this.userService = userService;
        }

        public async Task Create(string title, string image, string description, string authorName)
        {
            var author = this.userService.GetUserByName(authorName);
            await this.blogRepository.AddAsync(new Blog
            {
                Title = title,
                Image = image,
                Description = description,
                AuthorId = author.Id,
                CreatedOn = DateTime.UtcNow

            });
            await this.blogRepository.SaveChangesAsync();
        }

        public async Task DeleteBlogById(string id)
        {
            var blog = this.blogRepository.All().FirstOrDefault(blog => blog.Id == id);
            this.blogRepository.Delete(blog);
            await this.blogRepository.SaveChangesAsync();
        }

        public async Task EditBlogById(string id, string title, string image, string description)
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
