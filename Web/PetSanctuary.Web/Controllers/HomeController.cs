namespace PetSanctuary.Web.Controllers
{
    using System.Diagnostics;
    using PetSanctuary.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using PetSanctuary.Services.Data.Catalogs;
    using System.Linq;
    using PetSanctuary.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly ICatalogService catalogService;

        public HomeController(ICatalogService catalogService)
        {
            this.catalogService = catalogService;
        }

        public IActionResult Index()
        {
            var dogs = this.catalogService.GetAllDogs()
                .Select(x => new AllDogsHomeViewModel
                {
                    Id = x.Id,
                    Image = x.Image,
                    Name = x.Name
                }).ToList();
            return this.View(dogs);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
