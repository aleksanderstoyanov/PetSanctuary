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
           
            return this.View();
        }

        public IActionResult Cats()
        {
            return this.View();
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CatalogFormCreateViewModel model)
        {

            return this.Redirect("/Catalog/Dogs");
        }
    }
}
