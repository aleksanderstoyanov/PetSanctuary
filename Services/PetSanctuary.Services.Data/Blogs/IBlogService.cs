namespace PetSanctuary.Services.Data.Blogs
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IBlogService
    {
        IEnumerable<BlogServiceModel> GetAllBlogs();

        Task CreateAsync(string title, IFormFile image, string description, string authorName, string rootPath);

        IEnumerable<BlogServiceModel> GetAllUserBlogs(string id, int currentPage, int postsPerPage, bool isAdmin);

        BlogServiceModel GetBlogByTitle(string title);

        BlogServiceModel GetBlogById(string id);

        Task EditByIdAsync(string id, string title, IFormFile image, string description,string rootPath);

        Task DeleteByIdAsync(string id, string rootPath);
    }
}
