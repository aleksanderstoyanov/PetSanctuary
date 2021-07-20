using PetSanctuary.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Services.Data.Cities
{
    public interface ICityService
    {
        CityServiceModel GetCityByName(string name);

        CityServiceModel GetCityById(int id);

        Task Create(string name);
    }
}
