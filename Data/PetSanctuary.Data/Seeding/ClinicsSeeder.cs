namespace PetSanctuary.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using PetSanctuary.Data.Models;
    using PetSanctuary.Services.Data.Addresses;
    using PetSanctuary.Services.Data.Cities;

    public class ClinicsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Clinics.Any())
            {
                return;
            }

            var cityService = serviceProvider.GetService(typeof(ICityService)) as ICityService;
            var addressService = serviceProvider.GetService(typeof(IAddressService)) as IAddressService;

            await dbContext.Clinics.AddAsync(new Clinic
            {
                Name = "SinKrust",
                CityId = cityService.GetCityByName("Sofia").Id,
                AddressId = addressService.GetAddressByName("Pancharevo Chereshova gradina").Id,
                Image = "https://pomorievetclinic.com/wp-content/uploads/2015/11/Klinika-Vhod.jpg"

            });
            await dbContext.Clinics.AddAsync(new Clinic
            {
                Name = "Izgrev",
                CityId = cityService.GetCityByName("Burgas").Id,
                AddressId = addressService.GetAddressByName("zh.k.Izgrev 94,8008").Id,
                Image = "https://nacionalen-biznes.com/custom/domain_1/image_files/sitemgr_photo_129496.jpg"

            });

            await dbContext.Clinics.AddAsync(new Clinic
            {
                Name = "Diana",
                CityId = cityService.GetCityByName("Varna").Id,
                AddressId = addressService.GetAddressByName("19 Moryashka Street, 9003").Id,
                Image = "https://bgregistar.com/res/objects/14918/page/16_V%D0%B5t%D0%B5rinarna%20klinika%20DianaV%D0%B5t.jpg"

            });
        }
    }
}
