using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetSanctuary.Services.Data.Blogs;
using PetSanctuary.Services.Data.Comments;
using PetSanctuary.Services.Data.Users;
using PetSanctuary.Web.ViewModels.Blogs;
using PetSanctuary.Web.ViewModels.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PetSanctuary.Web.Controllers
{
    public class BlogsController : BaseController
    {
        private readonly IBlogService blogService;

        public BlogsController(IBlogService blogService)
        {
            this.blogService = blogService;
        }

        public IActionResult Index()
        {
            var model = this.blogService.GetAllBlogs();
            return this.View(model);
        }


        [Authorize]
        public IActionResult Create()
        {

            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(BlogFormCreateViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.blogService.Create(model.Title, model.Image, model.Description, this.User.Identity.Name);

            return this.Redirect($"/Blogs");
        }

    }
}
