namespace PetSanctuary.Services.Data.Comments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PetSanctuary.Data.Common.Repositories;
    using PetSanctuary.Data.Models;
    using PetSanctuary.Services.Mapping;

    public class CommentService : ICommentService
    {
        private readonly IDeletableEntityRepository<Comment> commentRepository;
        private readonly IRepository<BlogComment> blogCommentRepository;
        private readonly IRepository<VetComment> vetCommentRepository;

        public CommentService(
            IDeletableEntityRepository<Comment> commentRepository,
            IRepository<BlogComment> blogCommentRepository,
            IRepository<VetComment> vetCommentRepository)
        {
            this.commentRepository = commentRepository;
            this.blogCommentRepository = blogCommentRepository;
            this.vetCommentRepository = vetCommentRepository;
        }

        public async Task CreateAsync(string id, string content, string type, string publisherId)
        {
            var comment = new Comment
            {
                Content = content,
                PublishedOn = DateTime.UtcNow,
                PublisherId = publisherId,
            };

            if (type == "Blog")
            {
                comment.BlogComments.Add(new BlogComment
                {
                    BlogId = id,
                    CommentId = comment.Id,
                });
            }
            else
            {
                comment.VetComments.Add(new VetComment
                {
                    VetId = id,
                    CommentId = comment.Id,
                });
            }

            await this.commentRepository.AddAsync(comment);
            await this.commentRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var comment = this.commentRepository.All()
                .FirstOrDefault(comment => comment.Id == id);

            this.commentRepository.Delete(comment);
            await this.commentRepository.SaveChangesAsync();
        }

        public async Task EditAsync(int id, string content)
        {
            var comment = this.commentRepository.All()
                .FirstOrDefault(comment => comment.Id == id);

            comment.Content = content;
            await this.commentRepository.SaveChangesAsync();
        }

        public IEnumerable<CommentServiceModel> GetAllBlogComments(string blogId, int currentPage, int postsPerPage)
        {
            return this.commentRepository
              .AllAsNoTracking()
              .Where(comment => comment.BlogComments.Any(blogComment => blogComment.BlogId == blogId))
              .Skip((currentPage - 1) * postsPerPage)
              .Take(postsPerPage)
              .To<CommentServiceModel>()
              .ToList();
        }

        public IEnumerable<CommentServiceModel> GetAllVetComments(string vetId, int currentPage, int postsPerPage)
        {
            return this.commentRepository
              .AllAsNoTracking()
              .Where(comment => comment.VetComments.Any(vetComment => vetComment.VetId == vetId))
              .Skip((currentPage - 1) * postsPerPage)
              .Take(postsPerPage)
              .To<CommentServiceModel>()
              .ToList();
        }

        public string GetBlogIdByComment(int id)
        {
            return this.blogCommentRepository.All()
                 .FirstOrDefault(blogComment => blogComment.CommentId == id)
                 .BlogId;
        }

        public CommentServiceModel GetCommentById(int id)
        {
            return this.commentRepository
              .All()
              .Where(comment => comment.Id == id)
              .To<CommentServiceModel>()
              .FirstOrDefault();
        }

        public string GetIdByComment(int id, string type)
        {
            if (type == "Vet")
            {
                return this.vetCommentRepository
               .All()
               .FirstOrDefault(blogComment => blogComment.CommentId == id)
               .VetId;
            }

            return this.blogCommentRepository.All()
               .FirstOrDefault(blogComment => blogComment.CommentId == id)
               .BlogId;
        }

        public string GetVetIdByComment(int id)
        {
            return this.vetCommentRepository
                .All()
                .FirstOrDefault(blogComment => blogComment.CommentId == id)
                .VetId;
        }
    }
}
