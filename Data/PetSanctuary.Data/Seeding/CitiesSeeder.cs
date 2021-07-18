using Microsoft.EntityFrameworkCore;
using PetSanctuary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Data.Seeding
{
    public class CitiesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Cities.Any())
            {
                return;
            }


            await dbContext.AddAsync(new City
            {
                Name = "Sofia"
            });
            await dbContext.AddAsync(new City
            {
                Name = "Plovdiv"
            });
            await dbContext.AddAsync(new City
            {
                Name = "Varna"
            });
            await dbContext.AddAsync(new City
            {
                Name = "Burgas"
            });
            await dbContext.AddAsync(new City
            {
                Name = "Stara Zagora"
            });
            await dbContext.AddAsync(new City
            {
                Name = "Gabrovo"
            });

        }
    }
}
