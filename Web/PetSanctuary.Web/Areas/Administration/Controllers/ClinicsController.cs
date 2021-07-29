using Microsoft.AspNetCore.Mvc;
using PetSanctuary.Services.Data.Administration.Clinics;
using PetSanctuary.Services.Data.Clinics;
using PetSanctuary.Web.ViewModels.Administration.Clinics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSanctuary.Web.Areas.Administration.Controllers
{
    public class ClinicsController : AdministrationController
    {
        private readonly IAdminClinicService adminClinicService;
        private readonly IClinicService clinicService;

        public ClinicsController(IAdminClinicService adminClinicService, IClinicService clinicService)
        {
            this.adminClinicService = adminClinicService;
            this.clinicService = clinicService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClinicInputModel model)
        {
            await this.adminClinicService.CreateAsync(model.Name, model.Address, model.City, model.Image);
            return this.Redirect("/Clinics/Index");
        }

        public IActionResult Edit(int id)
        {
            var clinic = this.clinicService.GetClinicById(id);
            var model = new ClinicInputModel
            {
                Name = clinic.Name,
                Address = clinic.Address,
                City = clinic.City,
                Image = clinic.Image
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ClinicInputModel model)
        {
            await this.adminClinicService.EditAsync(id, model.Name, model.Address, model.City, model.Image);
            return this.Redirect("/Clinics/Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.adminClinicService.DeleteAsync(id);
            return this.Redirect("/Clinics/Index");
        }
    }
}
