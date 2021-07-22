using Microsoft.AspNetCore.Mvc;
using PetSanctuary.Services.Data.Addresses;
using PetSanctuary.Services.Data.Cities;
using PetSanctuary.Services.Data.Clinics;
using PetSanctuary.Services.Data.Vets;
using PetSanctuary.Web.ViewModels.Vets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSanctuary.Web.Controllers
{
    public class ClinicsController : BaseController
    {
        private readonly IClinicService clinicService;
        private readonly IVetService vetService;

        public ClinicsController(IClinicService clinicService, IVetService vetService)
        {
            this.clinicService = clinicService;
            this.vetService = vetService;
        }

        public IActionResult Index()
        {
            var model = this.clinicService.GetAllClinics().ToList();
            return this.View(model);
        }

        public IActionResult Vets(int id)
        {
            var model = this.vetService.GetVetsById(id)
                .Select(x => new VetsByIdViewModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    Surname = x.Surname,
                    Likes = x.Likes == null ? 0 : x.Likes,
                    Dislikes = x.Dislikes == null ? 0 : x.Dislikes
                }).ToList();
            return this.View(model);
        }

        public async Task<IActionResult> Like(string vetId)
        {
            var vet = this.vetService.GetVetById(vetId);
            await this.vetService.UpdateLikes(vetId);
            return this.Redirect($"/Clinics/Vets/{vet.ClinicId}");
        }

        public async Task<IActionResult> Dislike(string vetId)
        {
            var vet = this.vetService.GetVetById(vetId);
            await this.vetService.UpdateDislikes(vetId);
            return this.Redirect($"/Clinics/Vets/{vet.ClinicId}");
        }

        public IActionResult Description(string id)
        {
            var vet = this.vetService.GetVetById(id);
            var model = new VetsDetailViewModel
            {
                FirstName = vet.FirstName,
                Surname = vet.Surname,
                Description = vet.Description
            };

            return this.View(model);
        }
    }
}
