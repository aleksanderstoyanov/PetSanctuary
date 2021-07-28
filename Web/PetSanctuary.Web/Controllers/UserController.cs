using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetSanctuary.Common;
using PetSanctuary.Services.Data.Addresses;
using PetSanctuary.Services.Data.Blogs;
using PetSanctuary.Services.Data.Catalogs;
using PetSanctuary.Services.Data.Cities;
using PetSanctuary.Services.Data.Counts;
using PetSanctuary.Services.Data.Users;
using PetSanctuary.Web.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PetSanctuary.Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly ICatalogService catalogService;
        private readonly IUserService userService;
        private readonly IBlogService blogService;
        private readonly ICountService countService;

        public UserController(
            ICatalogService catalogService,
            IUserService userService,
            IBlogService blogService,
            ICountService countService)
        {
            this.catalogService = catalogService;
            this.userService = userService;
            this.blogService = blogService;
            this.countService = countService;
        }

        [Authorize]
        public IActionResult Profile()
        {

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = new ProfileViewModel
            {

                Email = this.User.Identity.Name,
                NumberOfPosts = this.countService.GetUserPostsCount(userId, this.User.IsInRole(GlobalConstants.AdministratorRoleName)),
                PhoneNumber = this.userService.GetUserPhoneNumber(userId),
                NumberOfBlogs = this.countService.GetUserBlogsCount(userId, this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            };
            return this.View(model);
        }

        [Authorize]
        public IActionResult Posts([FromQuery] PetPostQueryModel query)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool isAdmin = this.User.IsInRole(GlobalConstants.AdministratorRoleName);

            query.Pets = this.catalogService
               .GetAllUserPets(userId, query.CurrentPage, query.ElementsPerPage, isAdmin)
               .ToList();

            query.TotalPosts = this.countService.GetUserPostsCount(userId, isAdmin);

            return this.View(query);
        }

        [Authorize]
        public IActionResult EditPost(string id)
        {
            var pet = this.catalogService.GetPetById(id);
            var model = new PetPostViewModel
            {
                Name = pet.Name,
                Age = pet.Age,
                Image = pet.Image,
                Address = pet.Address,
                City = pet.City,
                Gender = pet.Gender.ToString(),
                Type = pet.Type.ToString(),
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditPost(string id, PetPostViewModel model)
        {
            if (model.Type != "Dog" && model.Type != "Cat" && model.Type != "Other")
            {
                this.ModelState.AddModelError(nameof(model.Type), "Pet type is invalid");
            }

            if (model.Gender != "Male" && model.Gender != "Female")
            {
                this.ModelState.AddModelError(nameof(model.Gender), "Pet gender is invalid");
            }

            if (model.IsVaccinated != "Yes" && model.Type != "No")
            {
                this.ModelState.AddModelError(nameof(model.IsVaccinated), "Pet's vaccination type is invalid");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.catalogService.EditPetById(id, model.Name, model.Age, model.Image, model.Type, model.Gender, model.IsVaccinated, model.City, model.Address);
            return this.RedirectToAction(nameof(this.Posts), "User");
        }

        [Authorize]

        public async Task<IActionResult> DeletePost(string id)
        {
            await this.catalogService.DeletePetById(id);
            return this.RedirectToAction(nameof(this.Posts), "User");
        }

        [Authorize]
        public IActionResult Blogs([FromQuery] BlogPostQueryModel query)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool isAdmin = this.User.IsInRole(GlobalConstants.AdministratorRoleName);

            var blogs = this.blogService
              .GetAllUserBlogs(userId, query.CurrentPage, query.ElementsPerPage, isAdmin)
              .ToList();

            query.Blogs = blogs;
            query.TotalPosts = this.countService.GetUserBlogsCount(userId, isAdmin);

            return this.View(query);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Blogs(string id, BlogPostViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.blogService.EditBlogById(id, model.Title, model.Image, model.Description);
            return this.RedirectToAction(nameof(this.Blogs), "User");
        }

        [Authorize]
        public IActionResult EditBlog(string id)
        {
            var blog = this.blogService.GetBlogById(id);
            var model = new BlogPostViewModel
            {
                Title = blog.Title,
                Image = blog.Image,
                Description = blog.Description

            };
            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditBlog(string id, BlogPostViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.blogService.EditBlogById(id, model.Title, model.Image, model.Description);
            return this.RedirectToAction(nameof(this.Blogs), "User");
        }

        [Authorize]
        public async Task<IActionResult> DeleteBlog(string id)
        {
            await this.blogService.DeleteBlogById(id);
            return this.RedirectToAction(nameof(this.Blogs), "User");
        }
    }
}
