﻿using PetSanctuary.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Services.Data.Catalogs
{
    public interface ICatalogService
    {
        Task Create(string name, int age, string image, string type, string cityName, string addressName, string isVaccinated);

        ICollection<Pet> GetAllDogs();

        ICollection<Pet> GetAllCats();

        Pet GetPetById(string id);
    }
}
