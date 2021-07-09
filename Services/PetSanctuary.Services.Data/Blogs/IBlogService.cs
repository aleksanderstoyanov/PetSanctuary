﻿using PetSanctuary.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Services.Data.Blogs
{
    public interface IBlogService
    {
        ICollection<Blog> GetAllBlogs();
        Task Create(string title, string image, string description, string authorName);

        Blog GetBlogByName(string name);

        Blog GetBlogById(string id);

        Task EditBlogById(string id, string title, string image, string description);
        Task DeleteBlogById(string id);
    }
}