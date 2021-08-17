namespace PetSanctuary.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using PetSanctuary.Services.Data.Administration.Vets;
    using PetSanctuary.Services.Data.Clinics;
    using PetSanctuary.Services.Data.Vets;
    using PetSanctuary.Web.ViewModels.Administration.Vets;

    public class VetsController : AdministrationController
    {
        private readonly IAdminVetService adminVetService;
        private readonly IClinicService clinicService;
        private readonly IVetService vetService;

        public VetsController(
            IAdminVetService adminVetService,
            IClinicService clinicService,
            IVetService vetService)
        {
            this.adminVetService = adminVetService;
            this.clinicService = clinicService;
            this.vetService = vetService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VetInputModel model)
        {
            var clinic = this.clinicService.GetClinicByName(model.Clinic);
            if (clinic == null)
            {
                this.ModelState.AddModelError(nameof(model.Clinic), "Clinic in non existing");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.adminVetService.CreateAsync(model.FirstName, model.Surname, model.Description, model.Qualification, model.Clinic);
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
                Clinic = vet.Clinic,
            };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int clinicId, string id, VetInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.adminVetService.EditAsync(id, model.FirstName, model.Surname, model.Description, model.Qualification, model.Clinic);
            return this.Redirect($"/Vets/Index/{clinicId}");
        }

        public async Task<IActionResult> Delete(int clinicId, string id)
        {
            await this.adminVetService.DeleteAsync(id);
            return this.Redirect($"/Vets/Index/{clinicId}");
        }
    }
}
