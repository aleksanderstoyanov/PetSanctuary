using PetSanctuary.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Services.Data.Users
{
    public interface IUserService
    {
        string GetUserPhoneNumber(string id);

        Task<ApplicationUser> GetUserByName(string name);

        Task<ApplicationUser> GetUserById(string id);
    }
}
