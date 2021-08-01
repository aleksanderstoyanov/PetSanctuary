namespace PetSanctuary.Services.Data.Blogs
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

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
