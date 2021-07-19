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
        private readonly ICityService cityService;
        private readonly IAddressService addressService;
        private readonly IUserService userService;

        public CatalogController(ICatalogService catalogService, ICityService cityService, IAddressService addressService, IUserService userService)
        {
            this.catalogService = catalogService;
            this.cityService = cityService;
            this.addressService = addressService;
            this.userService = userService;
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

        public IActionResult Other()
        {
            var others = this.catalogService.GetAllOthers()
                .Select(x => new CatalogViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image
                }).ToList();
            return this.View(others);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CatalogFormCreateViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.catalogService.Create(model.Name, model.Age, model.Image, model.Type, model.Gender, model.City, model.Address, model.IsVaccinated, this.User.Identity.Name);
            return this.Redirect("/Catalog/Dogs");
        }

        public IActionResult Details(string id)
        {
            var pet = this.catalogService.GetPetById(id);
            var model = new CatalogDetailsViewModel
            {
                Id = pet.Id,
                Name = pet.Name,
                Image = pet.Image,
                Address = this.addressService.GetAddressById(pet.AddressId).Name,
                Gender = pet.Gender.ToString(),
                City = this.cityService.GetCityById(pet.CityId).Name,
                CreatedOn = pet.CreatedOn.ToString(),
                IsVaccinated = pet.IsVaccinated ? "Yes" : "No",
                PhoneNumber = this.userService.GetUserById(pet.OwnerId).PhoneNumber
        };
            return this.View(model);
    }
}
}
