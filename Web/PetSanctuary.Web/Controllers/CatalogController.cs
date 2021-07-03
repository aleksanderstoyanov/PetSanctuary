using Microsoft.AspNetCore.Mvc;
using PetSanctuary.Services.Data.Catalogs;
using PetSanctuary.Web.ViewModels.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSanctuary.Web.Controllers
{
    public class CatalogController : Controller
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
                    Name = x.Name,
                    Image = x.Image

                }).ToList();
            return this.View(dogs);
        }

        public IActionResult Cats()
        {
            var cats = this.catalogService.GetAllCats()
               .Select(x => new CatalogViewModel
               {
                   Name = x.Name,
                   Image = x.Image

               }).ToList();
            return this.View(cats);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CatalogFormCreateViewModel model)
        {
            await this.catalogService.Create(model.Name, model.Age, model.Image, model.Type, model.City, model.Address, model.IsVaccinated);
            return this.Redirect("/Catalog/Dogs");
        }
    }
}
