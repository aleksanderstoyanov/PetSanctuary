using Microsoft.AspNetCore.Mvc;
using PetSanctuary.Services.Data.Addresses;
using PetSanctuary.Services.Data.Cities;
using PetSanctuary.Services.Data.Clinics;
using PetSanctuary.Services.Data.Vets;
using PetSanctuary.Web.ViewModels.Clinics;
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
        private readonly ICityService cityService;
        private readonly IAddressService addressService;
        private readonly IVetService vetService;

        public ClinicsController(IClinicService clinicService, ICityService cityService, IAddressService addressService, IVetService vetService)
        {
            this.clinicService = clinicService;
            this.cityService = cityService;
            this.addressService = addressService;
            this.vetService = vetService;
        }

        public IActionResult Index()
        {
            var model = this.clinicService.GetAllClinics()
                .Select(x => new ClinicsAllViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    City = this.cityService.GetCityById(x.CityId).Name,
                    Address = this.addressService.GetAddressById(x.AddressId).Name
                }).ToList();
            return this.View(model);
        }

        public IActionResult Vets(int id)
        {
            var model = this.vetService.GetVetsById(id)
                .Select(x => new VetsByIdViewModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    Surname = x.Surname
                }).ToList();
            return this.View(model);
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
