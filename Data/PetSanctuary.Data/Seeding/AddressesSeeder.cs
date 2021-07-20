
using PetSanctuary.Data.Models;
using PetSanctuary.Services.Data.Cities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Data.Seeding
{
    public class AddressesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Addresses.Any())
            {
                return;
            }

            var cityService = serviceProvider.GetService(typeof(ICityService)) as ICityService;
            await dbContext.Addresses.AddAsync(new Address
            {
                CityId = cityService.GetCityByName("Sofia").Id,
                Name = "Pancharevo Chereshova gradina"
            });
            await dbContext.Addresses.AddAsync(new Address
            {
                CityId = cityService.GetCityByName("Burgas").Id,
                Name = "zh.k.Izgrev 94,8008"
            });
            await dbContext.Addresses.AddAsync(new Address
            {
                CityId = cityService.GetCityByName("Varna").Id,
                Name = "19 Moryashka Street, 9003"
            });
            await dbContext.Addresses.AddAsync(new Address
            {
                CityId = cityService.GetCityByName("Sofia").Id,
                Name = "Simeonovsko Shosse Blvd."
            });
            await dbContext.Addresses.AddAsync(new Address
            {
                CityId = cityService.GetCityByName("Plovdiv").Id,
                Name = "zhk Trakia."
            });
            await dbContext.Addresses.AddAsync(new Address
            {
                CityId = cityService.GetCityByName("Sofia").Id,
                Name = "zhk Nadejda bl.172"
            });

        }
    }
}
