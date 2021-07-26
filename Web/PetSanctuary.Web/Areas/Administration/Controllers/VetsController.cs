using Microsoft.AspNetCore.Mvc;
using PetSanctuary.Services.Data.Administration.Vets;
using PetSanctuary.Services.Data.Clinics;
using PetSanctuary.Services.Data.Vets;
using PetSanctuary.Web.ViewModels.Administration.Vets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSanctuary.Web.Areas.Administration.Controllers
{
    public class VetsController : AdministrationController
    {
        private readonly IAdminVetService adminVetService;
        private readonly IVetService vetService;

        public VetsController(IAdminVetService adminVetService, IVetService vetService, IClinicService clinicService)
        {
            this.adminVetService = adminVetService;
            this.vetService = vetService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VetInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.adminVetService.Create(model.FirstName, model.Surname, model.Description, model.Qualification, model.Clinic);
            return this.Redirect("/Clinics/Index");
        }

        public IActionResult Edit(string id)
        {
            var vet = this.vetService.GetVetById(id);
            var model = new VetInputModel
            {
                FirstName = vet.FirstName,
                Surname = vet.Surname,
                Description = vet.Description,
                Qualification = "Veterinary",
                Clinic = vet.Clinic
            };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, VetInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.adminVetService.Edit(id, model.FirstName, model.Surname, model.Description, model.Qualification, model.Clinic);
            return this.Redirect("/Clinics/Index");
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.adminVetService.Delete(id);
            return this.Redirect("/Clinics/Index");
        }
    }
}
