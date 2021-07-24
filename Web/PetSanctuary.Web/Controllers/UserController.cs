using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetSanctuary.Services.Data.Addresses;
using PetSanctuary.Services.Data.Blogs;
using PetSanctuary.Services.Data.Catalogs;
using PetSanctuary.Services.Data.Cities;
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

        public UserController(ICatalogService catalogService, IUserService userService, IBlogService blogService)
        {
            this.catalogService = catalogService;
            this.userService = userService;
            this.blogService = blogService;
        }

        [Authorize]
        public IActionResult Profile()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = new ProfileViewModel
            {

                Email = this.User.Identity.Name,
                NumberOfPosts = this.catalogService.GetAllUserPets(userId).Count(),
                PhoneNumber = this.userService.GetUserPhoneNumber(userId),
                NumberOfBlogs = this.blogService.GetAllUserBlogs(userId).Count()
            };
            return this.View(model);
        }

        [Authorize]
        public IActionResult Posts()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var posts = this.catalogService.GetAllUserPets(userId)
                .Select(pet => new PetPostViewModel
                {
                    Id = pet.Id,
                    Name = pet.Name,
                    Age = pet.Age,
                    Address = pet.Address,
                    City = pet.City,
                    IsVaccinated = pet.IsVaccinated,
                    Type = pet.Type.ToString(),
                    Gender = pet.Gender.ToString(),
                    Image = pet.Image,
                    PhoneNumber = this.userService.GetUserPhoneNumber(userId),

                }).ToList();
            return this.View(posts);
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

            if (model.Gender != "Male" && model.Type != "Female")
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
        public IActionResult Blogs()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var blogs = this.blogService.GetAllUserBlogs(userId)
                .Select(x => new BlogPostViewModel
                {
                    Id = x.Id,
                    Image = x.Image,
                    Title = x.Title,
                    Description = x.Description

                }).ToList();
            return this.View(blogs);
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
