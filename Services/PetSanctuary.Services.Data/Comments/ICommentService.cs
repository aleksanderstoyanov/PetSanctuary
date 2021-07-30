using PetSanctuary.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Services.Data.Comments
{
    public interface ICommentService
    {
        CommentServiceModel GetCommentById(int id);

        string GetBlogIdByComment(int id);

        string GetVetIdByComment(int id);

        string GetIdByComment(int id, string type);

        Task CreateAsync(string id, string content, string type, string publisherId);

        Task EditAsync(int id, string content);

        Task DeleteAsync(int id);

        IEnumerable<CommentServiceModel> GetAllBlogComments(string blogId);

        IEnumerable<CommentServiceModel> GetAllVetComments(string vetId);
    }
}
