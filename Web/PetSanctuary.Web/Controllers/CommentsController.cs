using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetSanctuary.Services.Data.Comments;
using PetSanctuary.Web.ViewModels.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PetSanctuary.Web.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentService commentService;

        public CommentsController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        public IActionResult Blog(string id)
        {
            var model = this.commentService.GetAllBlogComments(id);
            this.ViewBag.Name = "Blog";
            return this.View("Comments", model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Blog(string id, CommentInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("Comments", model);
            }

            var publisherId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.commentService.CreateBlogCommentAsync(id, model.Content, publisherId);
            return this.RedirectToAction(nameof(this.Blog), "Comments");
        }

        [Authorize]
        [Route("/Comments/Blog/Edit/{id}")]
        public IActionResult EditBlogComment(int id)
        {
            var comment = this.commentService.GetCommentById(id);
            var model = new CommentInputModel
            {
                Content = comment.Content
            };

            return this.View("Edit", model);
        }

        [HttpPost]
        [Route("/Comments/Blog/Edit/{id}")]
        [Authorize]
        public async Task<IActionResult> EditBlogComment(int id, CommentInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("Edit", model);
            }

            var blogId = this.commentService.GetBlogIdByComment(id);
            await this.commentService.EditAsync(id, model.Content);
            return this.RedirectToAction(nameof(this.Blog), "Comments", new { id = blogId });
        }

        [Authorize]
        public async Task<IActionResult> DeleteBlogComment(int id)
        {
            var blogId = this.commentService.GetBlogIdByComment(id);
            await this.commentService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Blog), "Comments", new { id = blogId });
        }


        public IActionResult Vet(string id)
        {
            var model = this.commentService.GetAllVetComments(id);
            this.ViewBag.Name = "Vet";
            return this.View("Comments", model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Vet(string id, CommentInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("Comments", model);
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.commentService.CreateVetCommentAsync(id, model.Content, userId);
            return this.RedirectToAction(nameof(this.Vet), "Comments", new { id = id });
        }

        [Authorize]
        [Route("/Comments/Vet/Edit/{id}")]
        public IActionResult EditVetComment(int id)
        {
            var comment = this.commentService.GetCommentById(id);
            var model = new CommentInputModel
            {
                Content = comment.Content
            };

            return this.View("Edit", model);
        }

        [HttpPost]
        [Authorize]
        [Route("/Comments/Vet/Edit/{id}")]
        public async Task<IActionResult> EditVetComment(int id, CommentInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("Edit", model);
            }

            var vetId = this.commentService.GetVetIdByComment(id);
            await this.commentService.EditAsync(id, model.Content);
            return this.RedirectToAction(nameof(this.Vet), "Comments", new { id = vetId });
        }

        [Authorize]
        public async Task<IActionResult> DeleteVetComment(int id)
        {
            var vetId = this.commentService.GetVetIdByComment(id);
            await this.commentService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Vet), "Comments", new { id = vetId });
        }
    }
}
