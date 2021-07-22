using PetSanctuary.Data.Common.Repositories;
using PetSanctuary.Data.Models;
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
        private readonly IUserService userService;

        public CommentService(IDeletableEntityRepository<Comment> commentRepository, IUserService userService)
        {
            this.commentRepository = commentRepository;
            this.userService = userService;
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
            var comment = this.commentRepository.All().FirstOrDefault(comment => comment.Id == id);
            this.commentRepository.Delete(comment);
            await this.commentRepository.SaveChangesAsync();
        }

        public async Task Edit(int id, string content)
        {
            var comment = this.commentRepository.All().FirstOrDefault(comment => comment.Id == id);
            comment.Content = content;
            await this.commentRepository.SaveChangesAsync();
        }

        public IEnumerable<CommentServiceModel> GetAllBlogComments(string id)
        {
            return this.commentRepository.
                AllAsNoTracking()
                .Where(comment => comment.BlogId == id)
                .Select(comment => new CommentServiceModel
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    BlogId = comment.BlogId,
                    PublishedOn = comment.CreatedOn.ToString("ddd d MMM"),
                    Publisher = this.userService.GetUserById(comment.PublisherId).UserName
                })
                .ToList();
        }

        public CommentServiceModel GetCommentById(int id)
        {
            return this.commentRepository
                .All()
                .Where(comment => comment.Id == id)
                .Select(comment => new CommentServiceModel
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    BlogId = comment.BlogId,
                    PublishedOn = comment.CreatedOn.ToString("ddd d MMM"),
                    Publisher = this.userService.GetUserById(comment.PublisherId).UserName
                })
                .FirstOrDefault();
        }
    }
}
