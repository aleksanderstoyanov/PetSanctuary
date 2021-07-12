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



        public async Task Create(string blogId, string content, string publisherId)
        {
            await this.commentRepository.AddAsync(new Comment
            {
                BlogId = blogId,
                Content = content,
                PublishedOn = DateTime.UtcNow,
                PublisherId = publisherId

            });
            await this.commentRepository.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var comment = this.GetCommentById(id);
            this.commentRepository.Delete(comment);
            await this.commentRepository.SaveChangesAsync();
        }

        public async Task Edit(int id, string content)
        {
            var comment = this.GetCommentById(id);
            comment.Content = content;
            await this.commentRepository.SaveChangesAsync();
        }

        public ICollection<Comment> GetAllBlogComments(string id)
        {
            return this.commentRepository.AllAsNoTracking().Where(x => x.BlogId == id).ToList();
        }

        public Comment GetCommentById(int id)
        {
            return this.commentRepository.All().Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
