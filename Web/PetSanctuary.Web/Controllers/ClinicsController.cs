using Microsoft.AspNetCore.Mvc;
using PetSanctuary.Services.Data.Addresses;
using PetSanctuary.Services.Data.Cities;
using PetSanctuary.Services.Data.Clinics;
using PetSanctuary.Web.ViewModels.Clinics;
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

        public ClinicsController(IClinicService clinicService, ICityService cityService, IAddressService addressService)
        {
            this.clinicService = clinicService;
            this.cityService = cityService;
            this.addressService = addressService;
        }

        public IActionResult Index()
        {
            var model = this.clinicService.GetAllClinics()
                .Select(x => new ClinicsAllViewModel
                {
                    Name = x.Name,
                    Image = x.Image,
                    City = this.cityService.GetCityById(x.CityId).Name,
                    Address = this.addressService.GetAddressById(x.AddressId).Name
                }).ToList();
            return this.View(model);
        }
    }
}
