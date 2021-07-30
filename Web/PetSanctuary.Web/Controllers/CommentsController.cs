using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetSanctuary.Services.Data.Comments;
using PetSanctuary.Services.Data.Counts;
using PetSanctuary.Web.ViewModels.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PetSanctuary.Web.Controllers
{
    public class CommentsController : BaseController
    {
        private readonly ICommentService commentService;
        private readonly ICountService countService;

        public CommentsController(ICommentService commentService, ICountService countService)
        {
            this.commentService = commentService;
            this.countService = countService;
        }

        public IActionResult Blog(string id, [FromQuery] CommentQueryModel query)
        {
            var comments = this.commentService
                .GetAllBlogComments(id, query.CurrentPage, query.ElementsPerPage)
                .ToList();

            query.Comments = comments;
            query.TotalPosts = this.countService.GetBlogCommentsCount(id);
            this.ViewBag.Name = nameof(this.Blog);

            return this.View("Comments", query);
        }

        public IActionResult Vet(string id, [FromQuery] CommentQueryModel query)
        {
            var comments = this.commentService
                .GetAllVetComments(id, query.CurrentPage, query.ElementsPerPage)
                .ToList();
            query.Comments = comments;
            query.TotalPosts = this.countService.GetVetCommentsCount(id);

            this.ViewBag.Name = nameof(this.Vet);
            return this.View("Comments", query);
        }

        [Authorize]

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(string id, string type, CommentInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            var publisherId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.commentService.CreateAsync(id, model.Content, type, publisherId);

            return this.RedirectToAction(type, "Comments", new { id = id });

        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var comment = this.commentService.GetCommentById(id);
            var model = new CommentInputModel
            {
                Content = comment.Content
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, string type, CommentInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var typeId = this.commentService.GetIdByComment(id, type);
            await this.commentService.EditAsync(id, model.Content);
            return this.RedirectToAction(type, "Comments", new { id = typeId });
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id, string type)
        {
            var typeId = this.commentService.GetIdByComment(id, type);
            await this.commentService.DeleteAsync(id);
            return this.RedirectToAction(type, "Comments", new { id = typeId });
        }
    }
}
