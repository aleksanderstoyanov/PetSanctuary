using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetSanctuary.Services.Data.Addresses;
using PetSanctuary.Services.Data.Catalogs;
using PetSanctuary.Services.Data.Cities;
using PetSanctuary.Services.Data.Users;
using PetSanctuary.Web.ViewModels.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSanctuary.Web.Controllers
{
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
                    CreatedOn=x.CreatedOn
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
                   CreatedOn=x.CreatedOn
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
                    CreatedOn=x.CreatedOn
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

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.catalogService.Create(model.Name, model.Age, model.Image, model.Type, model.Gender, model.City, model.Address, model.IsVaccinated, this.User.Identity.Name);
            return this.RedirectToAction(nameof(this.Dogs), "Catalog");
        }

        public IActionResult Details(string id)
        {
            var model = this.catalogService.GetPetById(id);
            return this.View(model);
        }
    }
}
