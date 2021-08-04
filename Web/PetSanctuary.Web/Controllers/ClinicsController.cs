namespace PetSanctuary.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using PetSanctuary.Services.Data.Cities;
    using PetSanctuary.Services.Data.Clinics;
    using PetSanctuary.Services.Data.Comments;
    using PetSanctuary.Services.Data.Counts;
    using PetSanctuary.Services.Data.Vets;
    using PetSanctuary.Web.ViewModels.Clinics;
    using PetSanctuary.Web.ViewModels.Vets;

    public class ClinicsController : BaseController
    {
        private readonly IClinicService clinicService;
        private readonly ICityService cityService;
        private readonly ICountService countService;

        public ClinicsController(
            IClinicService clinicService,
            ICityService cityService,
            ICountService countService)
        {
            this.clinicService = clinicService;
            this.cityService = cityService;
            this.countService = countService;
        }

        public IActionResult Index([FromQuery] ClinicsQueryModel query)
        {
            if (this.cityService.GetCityByName(query.City) == null && query.City != "All")
            {
                this.ModelState.AddModelError(nameof(query.City), "City is invalid");
                query.City = "All";
                query.Clinics = this.clinicService.GetAllClinicsByCity(query.City, query.CurrentPage, query.ElementsPerPage).ToList();
                query.TotalPosts = this.countService.GetTotalClinicsCountByCity(query.City);
                return this.View(query);
            }

            var clinics = this.clinicService.GetAllClinicsByCity(query.City, query.CurrentPage, query.ElementsPerPage).ToList();
            query.TotalPosts = this.countService.GetTotalClinicsCountByCity(query.City);
            query.Clinics = clinics;

            return this.View(query);
        }
    }
}
