using PetSanctuary.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Services.Data.Cities
{
    public interface ICityService
    {
        City GetCityByName(string name);
        City GetCityById(int id);
        Task Create(string name);
    }
}
