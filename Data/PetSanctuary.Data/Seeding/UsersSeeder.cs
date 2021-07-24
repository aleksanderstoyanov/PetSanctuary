using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PetSanctuary.Common;
using PetSanctuary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Data.Seeding
{
    public class UsersSeeder : ISeeder
    {

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any(user => user.Email == GlobalConstants.AdministratorEmail))
            {
                return;
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var admin = new ApplicationUser
            {
                UserName = GlobalConstants.AdministratorEmail,
                Email = GlobalConstants.AdministratorEmail,
            };

            await userManager.CreateAsync(admin, "Kremigb22");
            if (!await userManager.IsInRoleAsync(admin, GlobalConstants.AdministratorRoleName))
            {
                await userManager.AddToRoleAsync(admin, GlobalConstants.AdministratorRoleName);
            }
        }
    }
}
