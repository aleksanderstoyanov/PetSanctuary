namespace PetSanctuary.Services.Data.Comments
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICommentService
    {
        CommentServiceModel GetCommentById(int id);

        string GetBlogIdByComment(int id);

        string GetVetIdByComment(int id);

        string GetIdByComment(int id, string type);

        Task CreateAsync(string id, string content, string type, string publisherId);

        Task EditAsync(int id, string content);

        Task DeleteAsync(int id);

        IEnumerable<CommentServiceModel> GetAllBlogComments(string blogId, int currentPage, int postsPerPage);

        IEnumerable<CommentServiceModel> GetAllVetComments(string vetId, int currentPage, int postsPerPage);
    }
}
