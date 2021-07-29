using PetSanctuary.Data.Common.Repositories;
using PetSanctuary.Data.Models;
using PetSanctuary.Services.Data.Addresses;
using PetSanctuary.Services.Data.Cities;
using PetSanctuary.Services.Data.Clinics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Services.Data.Administration.Clinics
{
    public class AdminClinicService : IAdminClinicService
    {
        private readonly IDeletableEntityRepository<Clinic> clinicRepository;
        private readonly IAddressService addressService;
        private readonly ICityService cityService;

        public AdminClinicService(IDeletableEntityRepository<Clinic> clinicRepository, IAddressService addressService, ICityService cityService)
        {
            this.clinicRepository = clinicRepository;
            this.addressService = addressService;
            this.cityService = cityService;
        }

        public async Task CreateAsync(string name, string addressName, string cityName, string image)
        {
            var city = this.cityService.GetCityByName(cityName);
            var address = this.addressService.GetAddressByName(addressName);
            if (city == null)
            {
                city = await this.EnsureCityCreated(cityName);
            }

            if (address == null)
            {
                address = await this.EnsureAddressCreated(addressName, city.Id);
            }

            await this.clinicRepository.AddAsync(new Clinic
            {
                Name = name,
                AddressId = address.Id,
                CityId = city.Id,
                Image = image
            });

            await this.clinicRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            this.clinicRepository.Delete(this.clinicRepository.All()
                 .FirstOrDefault(clinic => clinic.Id == id));
            await this.clinicRepository.SaveChangesAsync();
        }

        public async Task EditAsync(int id, string name, string addressName, string cityName, string image)
        {
            var city = this.cityService.GetCityByName(cityName);
            var address = this.addressService.GetAddressByName(addressName);
            if (city == null)
            {
                city = await this.EnsureCityCreated(cityName);
            }

            if (address == null)
            {
                address = await this.EnsureAddressCreated(addressName, city.Id);
            }

            var clinic = this.clinicRepository.All().FirstOrDefault(clinic => clinic.Id == id);
            clinic.Name = name;
            clinic.Image = image;
            clinic.AddressId = address.Id;
            clinic.CityId = city.Id;

            await this.clinicRepository.SaveChangesAsync();
        }

        private async Task<AddressServiceModel> EnsureAddressCreated(string addressName, int cityId)
        {
            await this.addressService.CreateAsync(addressName, cityId);
            return this.addressService.GetAddressByName(addressName);
        }

        private async Task<CityServiceModel> EnsureCityCreated(string cityName)
        {
            await this.cityService.CreateAsync(cityName);
            return this.cityService.GetCityByName(cityName);
        }
    }
}
