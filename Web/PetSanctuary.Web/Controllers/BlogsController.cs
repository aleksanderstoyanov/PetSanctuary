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
        private readonly ICommentService commentService;

        public BlogsController(IBlogService blogService, ICommentService commentService)
        {

            this.blogService = blogService;
            this.commentService = commentService;
        }

        public IActionResult Index()
        {
            var model = this.blogService.GetAllBlogs();
            return this.View(model);
        }

        public IActionResult Comments(string id)
        {
            var comments = this.commentService.GetAllBlogComments(id);
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

        public IActionResult EditComment(int id)
        {
            var comment = this.commentService.GetCommentById(id);
            var model = new CommentFormCreateViewModel
            {
                Content = comment.Content
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditComment(int id, CommentFormCreateViewModel model)
        {
            var blogId = this.commentService.GetCommentById(id).BlogId;
            await this.commentService.Edit(id, model.Content);
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
