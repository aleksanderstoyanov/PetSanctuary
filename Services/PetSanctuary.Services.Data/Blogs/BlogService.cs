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

        public Task DeleteBlogById(string id)
        {
            throw new NotImplementedException();
        }

        public Task EditBlogById(string id, string title, string image, string description)
        {
            throw new NotImplementedException();
        }

        public ICollection<Blog> GetAllBlogs()
        {
            return this.blogRepository.All().ToList();
        }

        public Blog GetBlogById(string id)
        {
            throw new NotImplementedException();
        }

        public Blog GetBlogByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
