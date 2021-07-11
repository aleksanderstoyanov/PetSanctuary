using PetSanctuary.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Services.Data.Comments
{
    public interface ICommentService
    {
        Comment GetCommentById(int id);
        Task Create();

        Task Edit(int id, string content);

        Task Delete(int id);

        ICollection<Comment> GetAllBlogComments(string id);

    }
}
