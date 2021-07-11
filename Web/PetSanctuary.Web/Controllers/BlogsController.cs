using Microsoft.AspNetCore.Mvc;
using PetSanctuary.Services.Data.Blogs;
using PetSanctuary.Services.Data.Comments;
using PetSanctuary.Services.Data.Users;
using PetSanctuary.Web.ViewModels.Blogs;
using PetSanctuary.Web.ViewModels.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSanctuary.Web.Controllers
{
    public class BlogsController : BaseController
    {
        private readonly IBlogService blogService;
        private readonly IUserService userService;
        private readonly ICommentService commentService;

        public BlogsController(IBlogService blogService, IUserService userService, ICommentService commentService)
        {

            this.blogService = blogService;
            this.userService = userService;
            this.commentService = commentService;
        }

        public IActionResult Index()
        {
            var model = this.blogService
                .GetAllBlogs()
                .Select(x => new BlogViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Image = x.Image,
                    Description = x.Description,
                    Author = this.userService.GetUserById(x.AuthorId).UserName,
                    CreatedOn = x.CreatedOn.ToString(),

                }).ToList();
            return this.View(model);
        }

        public IActionResult Comments(string id)
        {
            var comments = this.commentService.GetAllBlogComments(id)
                .Select(x => new CommentViewModel
                {
                    Id = x.Id,
                    Content = x.Content,
                    PublishedOn = x.CreatedOn.ToString(),
                    Publisher = this.userService.GetUserById(x.PublisherId).UserName
                }).ToList();
            return this.View(comments);
        }

        [HttpPost]
        public async Task<IActionResult> Comments(string id, CommentFormCreateViewModel model)
        {
            await this.blogService.AddCommentToBlog(id, model.Content, this.User.Identity.Name);
            return this.Redirect($"/Blogs/Comments/{id}");
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogFormCreateViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.blogService.Create(model.Title, model.Image, model.Description, this.User.Identity.Name);

            return this.Redirect("/Blogs");
        }

        [HttpPost]
        public async Task<IActionResult> EditComment(int id, string content)
        {
            var blogId = this.commentService.GetCommentById(id).BlogId;
            await this.commentService.Edit(id, content);
            return this.Redirect($"/Blogs/Comments/{blogId}");
        }

        public async Task<IActionResult> DeleteComment(int id)
        {
            var blogId = this.commentService.GetCommentById(id).BlogId;
            await this.commentService.Delete(id);
            return this.Redirect($"/Blogs/Comments/{blogId}");
        }

    }
}
