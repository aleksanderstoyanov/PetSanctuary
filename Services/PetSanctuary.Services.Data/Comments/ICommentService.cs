using PetSanctuary.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Services.Data.Comments
{
    public interface ICommentService
    {
        Task Create();
        ICollection<Comment> GetAllBlogComments(string id);
    }
}
