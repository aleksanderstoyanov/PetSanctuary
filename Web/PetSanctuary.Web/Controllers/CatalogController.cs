using Microsoft.AspNetCore.Mvc;
using PetSanctuary.Services.Data.Addresses;
using PetSanctuary.Services.Data.Catalogs;
using PetSanctuary.Services.Data.Cities;
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
        private readonly ICityService cityService;
        private readonly IAddressService addressService;

        public CatalogController(ICatalogService catalogService, ICityService cityService, IAddressService addressService)
        {
            this.catalogService = catalogService;
            this.cityService = cityService;
            this.addressService = addressService;
        }

        public IActionResult Dogs()
        {
            var dogs = this.catalogService.GetAllDogs()
                .Select(x => new CatalogViewModel
                {
                    Id = x.Id,
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
                   Id = x.Id,
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
        public IActionResult Details(string modelId)
        {
            var pet = this.catalogService.GetPetById(modelId);
            var model = new CatalogDetailsViewModel
            {
                Id = pet.Id,
                Name = pet.Name,
                Image = pet.Image,
                Address = this.addressService.GetAddressById(pet.AddressId).Name,
                City = this.cityService.GetCityById(pet.CityId).Name,
                CreatedOn = pet.CreatedOn.ToString(),
                IsVaccinated = pet.IsVaccinated ? "Yes" : "No",

            };
            return this.View(model);
        }
    }
}
