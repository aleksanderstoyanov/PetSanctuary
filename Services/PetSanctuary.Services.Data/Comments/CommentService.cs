using PetSanctuary.Data.Common.Repositories;
using PetSanctuary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Services.Data.Comments
{
    public class CommentService : ICommentService
    {
        private readonly IDeletableEntityRepository<Comment> commentRepository;

        public CommentService(IDeletableEntityRepository<Comment> commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public Task Create()
        {
            throw new NotImplementedException();
        }

        public ICollection<Comment> GetAllBlogComments(string id)
        {
            return this.commentRepository.All().Where(x => x.IsDeleted == false && x.BlogId == id).ToList();
        }
    }
}
