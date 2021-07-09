using Microsoft.AspNetCore.Mvc;
using PetSanctuary.Services.Data.Blogs;
using PetSanctuary.Services.Data.Users;
using PetSanctuary.Web.ViewModels.Blogs;
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

        public BlogsController(IBlogService blogService, IUserService userService)
        {

            this.blogService = blogService;
            this.userService = userService;
        }
        public IActionResult Index()
        {
            var model = this.blogService
                .GetAllBlogs()
                .Select(x => new BlogViewModel
                {
                    Title = x.Title,
                    Image = x.Image,
                    Description = x.Description,
                    Author=this.userService.GetUserById(x.AuthorId).UserName,
                    CreatedOn = x.CreatedOn.ToString(),

                }).ToList();
            return this.View(model);
        }
        public IActionResult Comments()
        {
            return this.View();
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

    }
}
