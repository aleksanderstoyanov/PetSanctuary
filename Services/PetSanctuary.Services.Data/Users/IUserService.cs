using PetSanctuary.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetSanctuary.Services.Data.Users
{
    public interface IUserService
    {
        ApplicationUser GetUserByName(string name);
    }
}
