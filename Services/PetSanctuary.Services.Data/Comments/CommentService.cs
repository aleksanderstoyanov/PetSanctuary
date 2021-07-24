using PetSanctuary.Data.Common.Repositories;
using PetSanctuary.Data.Models;
using PetSanctuary.Services.Data.Blogs;
using PetSanctuary.Services.Data.Users;
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
        private readonly IRepository<BlogComment> blogCommentRepository;
        private readonly IRepository<VetComment> vetCommentRepository;
        private readonly IUserService userService;

        public CommentService(
            IDeletableEntityRepository<Comment> commentRepository,
            IRepository<BlogComment> blogCommentRepository,
            IRepository<VetComment> vetCommentRepository,
            IUserService userService)
        {
            this.commentRepository = commentRepository;
            this.blogCommentRepository = blogCommentRepository;
            this.vetCommentRepository = vetCommentRepository;
            this.userService = userService;
        }

        public async Task CreateBlogComment(string blogId, string content, string publisherId)
        {
            var comment = new Comment
            {
                Content = content,
                PublishedOn = DateTime.UtcNow,
                PublisherId = publisherId,
            };
            comment.BlogComments.Add(new BlogComment
            {
                BlogId = blogId,
                CommentId = comment.Id
            });
            await this.commentRepository.AddAsync(comment);
            await this.commentRepository.SaveChangesAsync();
        }

        public async Task CreateVetComment(string vetId, string content, string publisherId)
        {
            var comment = new Comment
            {
                Content = content,
                PublishedOn = DateTime.UtcNow,
                PublisherId = publisherId,
            };
            comment.VetComments.Add(new VetComment
            {
                VetId = vetId,
                CommentId = comment.Id
            });
            await this.commentRepository.AddAsync(comment);
            await this.commentRepository.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var comment = this.commentRepository.All()
                .FirstOrDefault(comment => comment.Id == id);

            this.commentRepository.Delete(comment);
            await this.commentRepository.SaveChangesAsync();
        }

        public async Task Edit(int id, string content)
        {
            var comment = this.commentRepository.All()
                .FirstOrDefault(comment => comment.Id == id);

            comment.Content = content;
            await this.commentRepository.SaveChangesAsync();
        }

        public IEnumerable<CommentServiceModel> GetAllBlogComments(string blogId)
        {
            return this.MapComments(this.commentRepository.AllAsNoTracking()
                 .Where(comment => comment.BlogComments.Any(blogComment => blogComment.BlogId == blogId)))
                 .ToList();
        }

        public IEnumerable<CommentServiceModel> GetAllVetComments(string vetId)
        {
            return this.MapComments( this.commentRepository.AllAsNoTracking()
                 .Where(comment => comment.VetComments.Any(vetComment => vetComment.VetId == vetId)))
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
            return this.MapComments(this.commentRepository.All().Where(comment => comment.Id == id))
                .FirstOrDefault();
        }

        public string GetVetIdByComment(int id)
        {
            return this.vetCommentRepository
                .All()
                .FirstOrDefault(blogComment => blogComment.CommentId == id)
                .VetId;
        }

        private IEnumerable<CommentServiceModel> MapComments(IQueryable<Comment> comments)
        {
            return comments
                .Select(comment => new CommentServiceModel
                {
                    Id = comment.Id,
                    PublishedOn = comment.PublishedOn.ToString("ddd d MMM"),
                    Publisher = this.userService.GetUserById(comment.PublisherId).UserName,
                    Content = comment.Content
                }).ToList();
        }
    }
}
