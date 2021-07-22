using Microsoft.AspNetCore.Identity;
using PetSanctuary.Data.Common.Repositories;
using PetSanctuary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Services.Data.Users
{
    public class UserService : IUserService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(IDeletableEntityRepository<ApplicationUser> userRepository,UserManager<ApplicationUser> userManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
           return await this.userManager.FindByIdAsync(id);
        }

        public async Task<ApplicationUser> GetUserByName(string name)
        {
            return await this.userManager.FindByNameAsync(name);
        }

        public string GetUserPhoneNumber(string id)
        {
            var user = this.userManager.FindByIdAsync(id);
            return user.Result.PhoneNumber;
        }
    }
}
