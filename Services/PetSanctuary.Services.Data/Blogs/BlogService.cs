﻿using PetSanctuary.Data.Common.Repositories;
using PetSanctuary.Data.Models;
using PetSanctuary.Services.Data.Comments;
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
        private readonly ICommentService commentService;

        public BlogService(IDeletableEntityRepository<Blog> blogRepository, IUserService userService, ICommentService commentService)
        {
            this.blogRepository = blogRepository;
            this.userService = userService;
            this.commentService = commentService;
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
            this.blogRepository.Delete(blog);
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
        public async Task AddCommentToBlog(string blogId, string content, string username)
        {
            await this.EnsureCommentCreated(blogId, content, this.userService.GetUserByName(username).Id);
            await this.blogRepository.SaveChangesAsync();
        }

        public ICollection<Blog> GetAllBlogs()
        {
            return this.blogRepository.AllAsNoTracking().ToList();
        }

        public ICollection<Blog> GetAllUserBlogs(string id)
        {
            return this.blogRepository.AllAsNoTracking().Where(x => x.AuthorId == id).ToList();
        }

        public Blog GetBlogById(string id)
        {
            return this.blogRepository.All().FirstOrDefault(x => x.Id == id);
        }

        public Blog GetBlogByTitle(string title)
        {
            return this.blogRepository.AllAsNoTracking().FirstOrDefault(x => x.Title == title);
        }
        private async Task EnsureCommentCreated(string blogId, string content, string publisherId)
        {
            await this.commentService.Create(blogId, content, publisherId);
        }
    }
}
