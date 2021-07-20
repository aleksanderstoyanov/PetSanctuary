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

        Task Create(string blogId, string content, string publisherId);

        Task Edit(int id, string content);

        Task Delete(int id);

        IEnumerable<CommentServiceModel> GetAllBlogComments(string id);

    }
}
