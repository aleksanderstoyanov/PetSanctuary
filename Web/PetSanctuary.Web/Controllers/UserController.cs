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
using System.Threading.Tasks;

namespace PetSanctuary.Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly ICatalogService catalogService;
        private readonly IUserService userService;
        private readonly ICityService cityService;
        private readonly IAddressService addressService;
        private readonly IBlogService blogService;

        public UserController(ICatalogService catalogService, IUserService userService, ICityService cityService, IAddressService addressService, IBlogService blogService)
        {
            this.catalogService = catalogService;
            this.userService = userService;
            this.cityService = cityService;
            this.addressService = addressService;
            this.blogService = blogService;
        }

        public IActionResult Profile()
        {
            return this.View();
        }

        public IActionResult Posts()
        {
            var user = this.userService.GetUserByName(this.User.Identity.Name);
            var posts = this.catalogService.GetAllUserPets(user.Id)
                .Select(x => new PetPostViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Age = x.Age,
                    Address = this.addressService.GetAddressById(x.AddressId).Name,
                    City = this.cityService.GetCityById(x.CityId).Name,
                    IsVaccinated = x.IsVaccinated ? "Yes" : "No",
                    Type = x.Type.ToString(),
                    Gender = x.Gender.ToString(),
                    Image = x.Image
                }).ToList();
            return this.View(posts);
        }


        [HttpPost]
        public async Task<IActionResult> Posts(string id, PetPostViewModel model)
        {
            await this.catalogService.EditPetById(id, model.Name, model.Age, model.Image, model.Type, model.Gender, model.IsVaccinated, model.City, model.Address);
            return this.Redirect("/User/Posts");
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.catalogService.DeletePetById(id);
            return this.Redirect("/User/Posts");
        }
        public IActionResult Blogs()
        {
            var user = this.userService.GetUserByName(this.User.Identity.Name);
            var blogs = this.blogService.GetAllUserBlogs(user.Id)
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
        public async Task<IActionResult> Blogs(string id, BlogPostViewModel model)
        {
           
            await this.blogService.EditBlogById(id, model.Title, model.Image, model.Description);
            return this.Redirect("/User/Blogs");
        }

        [HttpGet("User/Blogs/{id}")]
        public async Task<IActionResult> Blogs(string id)
        {
            await this.blogService.DeleteBlogById(id);
            return this.Redirect("/User/Blogs");
        }
    }
}
