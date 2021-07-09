using PetSanctuary.Data.Common.Repositories;
using PetSanctuary.Data.Models;
using PetSanctuary.Services.Data.Users;
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
            var blog = this.GetBlogById(id);
            blog.IsDeleted = true;
            blog.DeletedOn = DateTime.UtcNow;
            await this.blogRepository.SaveChangesAsync();
        }

        public async Task EditBlogById(string id, string title, string image, string description)
        {
            var blog = this.GetBlogById(id);
            blog.Title = title;
            blog.Image = image;
            blog.Description = description;

            await this.blogRepository.SaveChangesAsync();

        }

        public ICollection<Blog> GetAllBlogs()
        {
            return this.blogRepository.All().Where(x => x.IsDeleted == false).ToList();
        }

        public ICollection<Blog> GetAllUserBlogs(string id)
        {
            return this.blogRepository.All().Where(x => x.AuthorId == id && x.IsDeleted == false).ToList();
        }

        public Blog GetBlogById(string id)
        {
            return this.blogRepository.All().FirstOrDefault(x => x.Id == id);
        }

        public Blog GetBlogByTitle(string title)
        {
            return this.blogRepository.All().FirstOrDefault(x => x.Title == title);
        }
    }
}
