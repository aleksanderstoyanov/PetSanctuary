using PetSanctuary.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Services.Data.Blogs
{
    public interface IBlogService
    {
        IEnumerable<BlogServiceModel> GetAllBlogs();

        Task Create(string title, string image, string description, string authorName);

        IEnumerable<BlogServiceModel> GetAllUserBlogs(string id);

        BlogServiceModel GetBlogByTitle(string title);

        BlogServiceModel GetBlogById(string id);

        Task EditBlogById(string id, string title, string image, string description);

        Task DeleteBlogById(string id);
    }
}
