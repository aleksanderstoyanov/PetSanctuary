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

        Task CreateAsync(string title, string image, string description, string authorName);

        IEnumerable<BlogServiceModel> GetAllUserBlogs(string id, int currentPage, int postsPerPage, bool isAdmin);

        BlogServiceModel GetBlogByTitle(string title);

        BlogServiceModel GetBlogById(string id);

        Task EditByIdAsync(string id, string title, string image, string description);

        Task DeleteByIdAsync(string id);
    }
}
