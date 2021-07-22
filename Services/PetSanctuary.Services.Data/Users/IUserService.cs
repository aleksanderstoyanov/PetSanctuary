﻿using PetSanctuary.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Services.Data.Users
{
    public interface IUserService
    {
        string GetUserPhoneNumber(string id);

        ApplicationUser GetUserByName(string name);

        ApplicationUser GetUserById(string id);
    }
}
