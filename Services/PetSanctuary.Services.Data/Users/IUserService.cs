using PetSanctuary.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetSanctuary.Services.Data.Users
{
    public interface IUserService
    {
        string GetUserPhoneNumber(string name);

        ApplicationUser GetUserByName(string name);

        ApplicationUser GetUserById(string id);
    }
}
