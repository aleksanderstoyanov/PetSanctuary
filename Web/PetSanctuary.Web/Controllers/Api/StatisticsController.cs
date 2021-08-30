namespace PetSanctuary.Web.Controllers.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using PetSanctuary.Services.Data.Counts;
    using PetSanctuary.Web.ViewModels.Api.Statistics;

    [ApiController]
    [Route("api/statistics")]
    public class StatisticsController : ControllerBase
    {
        private readonly ICountService countService;

        public StatisticsController(ICountService countService)
        {
            this.countService = countService;
        }

        [HttpGet]
        public ActionResult<StatisticsResponseModel> GetStatistics()
        {
            var totalDogs = this.countService.GetTotalPetsByType("Dog");
            var totalCats = this.countService.GetTotalPetsByType("Cat");
            var totalOthers = this.countService.GetTotalPetsByType("Other");
            return new StatisticsResponseModel()
            {
                TotalDogs = totalDogs,
                TotalCats = totalCats,
                TotalOthers = totalOthers,
            };
        }
    }
}
