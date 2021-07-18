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
                Image = "https://static.framar.bg/filestore/191227095707screenshot_4.jpg"

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

            await dbContext.Clinics.AddAsync(new Clinic
            {
                Name = "Central Veterinary Clinic",
                CityId = cityService.GetCityByName("Sofia").Id,
                AddressId = addressService.GetAddressByName("Simeonovsko Shosse Blvd.").Id,
                Image = "http://www.bgmedicalcatalog.com/image/cache/data/sg10_3-500x500.JPG"

            });

            await dbContext.Clinics.AddAsync(new Clinic
            {
                Name = "Vita Vet",
                CityId = cityService.GetCityByName("Sofia").Id,
                AddressId = addressService.GetAddressByName("zhk Nadejda bl.172").Id,
                Image = "https://vitabg.com/wp-content/uploads/2017/03/%D0%92%D0%B5%D1%82%D0%B5%D1%80%D0%B8%D0%BD%D0%B0%D1%80%D0%BD%D0%B0-%D0%BA%D0%BB%D0%B8%D0%BD%D0%B8%D0%BA%D0%B0-%D0%92%D0%B8%D1%82%D0%B0-%D1%81%D0%BB%D0%B0%D0%B9%D0%B41.jpg"
            });
            await dbContext.Clinics.AddAsync(new Clinic
            {
                Name = "Provet Clinic",
                CityId = cityService.GetCityByName("Plovdiv").Id,
                AddressId = addressService.GetAddressByName("zhk Trakia.").Id,
                Image = "https://businessregistar.com/wp-content/uploads/2019/11/1907809_583701001700034_1564485870_o.jpg"
            });


        }
    }
}
