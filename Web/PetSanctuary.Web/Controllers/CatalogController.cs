﻿namespace PetSanctuary.Web.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PetSanctuary.Common;
    using PetSanctuary.Services.Data.Catalogs;
    using PetSanctuary.Web.ViewModels.Catalog;

    public class CatalogController : BaseController
    {
        private readonly ICatalogService catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            this.catalogService = catalogService;
        }

        public IActionResult Dogs()
        {
            var dogs = this.catalogService.GetAllDogs()
                .Select(x => new CatalogViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    CreatedOn = x.CreatedOn,
                }).ToList();
            return this.View(dogs);
        }

        public IActionResult Cats()
        {
            var cats = this.catalogService.GetAllCats()
               .Select(x => new CatalogViewModel
               {
                   Id = x.Id,
                   Name = x.Name,
                   Image = x.Image,
                   CreatedOn = x.CreatedOn,
               }).ToList();
            return this.View(cats);
        }

        public IActionResult Other()
        {
            var others = this.catalogService.GetAllOthers()
                .Select(x => new CatalogViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    CreatedOn = x.CreatedOn,
                }).ToList();
            return this.View(others);
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CatalogFormCreateViewModel model)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (model.Type != "Dog" && model.Type != "Cat" && model.Type != "Other")
            {
                this.ModelState.AddModelError(nameof(model.Type), "Pet type is invalid");
            }

            if (model.Gender != "Male" && model.Type != "Female")
            {
                this.ModelState.AddModelError(nameof(model.Gender), "Pet gender is invalid");
            }

            if (model.IsVaccinated != "Yes" && model.IsVaccinated != "No")
            {
                this.ModelState.AddModelError(nameof(model.IsVaccinated), "Pet's vaccination is invalid");
            }

            if (model.Image != null)
            {
                var extension = Path.GetExtension(model.Image.FileName);
                if (extension != ".jpeg" && extension != ".jpg" && extension != ".gif" && extension != ".png")
                {
                    this.ModelState.AddModelError(nameof(model.Image), "Allowed file extensions are jpeg, jpg, gif and png");
                }
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.catalogService.Create(model.Name, model.Age, model.Image, model.Type, model.Gender, model.City, model.Address, model.IsVaccinated, userId, GlobalConstants.WwwRootPath);
            return this.RedirectToAction(nameof(this.Dogs), "Catalog");
        }

        [Authorize]
        public IActionResult Edit(string id)
        {
            var pet = this.catalogService.GetPetById(id);
            var model = new CatalogEditFormModel
            {
                Name = pet.Name,
                Age = pet.Age,
                Address = pet.Address,
                City = pet.City,
                Gender = pet.Gender.ToString(),
                Type = pet.Type.ToString(),
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(string id, CatalogEditFormModel model)
        {
            if (model.Type != "Dog" && model.Type != "Cat" && model.Type != "Other")
            {
                this.ModelState.AddModelError(nameof(model.Type), "Pet type is invalid");
            }

            if (model.Gender != "Male" && model.Gender != "Female")
            {
                this.ModelState.AddModelError(nameof(model.Gender), "Pet gender is invalid");
            }

            if (model.Image != null)
            {
                var extension = Path.GetExtension(model.Image.FileName);
                if (extension != ".jpeg" && extension != ".jpg" && extension != ".gif" && extension != ".png")
                {
                    this.ModelState.AddModelError(nameof(model.Image), "Allowed file extensions are jpeg, jpg, gif and png");
                }
            }

            if (model.IsVaccinated != "Yes" && model.IsVaccinated != "No")
            {
                this.ModelState.AddModelError(nameof(model.IsVaccinated), "Pet's vaccination type is invalid");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.catalogService.EditPetById(id, model.Name, model.Age, model.Image, model.Type, model.Gender, model.IsVaccinated, model.City, model.Address, GlobalConstants.WwwRootPath);
            if (this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.RedirectToAction(nameof(this.Dogs), "Catalog");
            }

            return this.RedirectToAction("Posts", "MyProfile");
        }

        [Authorize]

        public async Task<IActionResult> Delete(string id)
        {
            await this.catalogService.DeletePetById(id, GlobalConstants.WwwRootPath);
            if (this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.RedirectToAction(nameof(this.Dogs), "Catalog");
            }

            return this.RedirectToAction("Posts", "MyProfile");
        }

        public IActionResult Details(string id)
        {
            var model = this.catalogService.GetPetById(id);
            return this.View(model);
        }
    }
}
