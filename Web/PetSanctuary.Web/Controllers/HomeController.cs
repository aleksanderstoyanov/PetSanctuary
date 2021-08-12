namespace PetSanctuary.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using PetSanctuary.Services.Data.Catalogs;
    using PetSanctuary.Web.ViewModels;
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
            var dogs = this.catalogService.GetAllPets()
                .Select(pet => new AllDogsHomeViewModel
                {
                    Id = pet.Id,
                    Image = pet.Image,
                    Name = pet.Name,
                })
                .ToList();
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
