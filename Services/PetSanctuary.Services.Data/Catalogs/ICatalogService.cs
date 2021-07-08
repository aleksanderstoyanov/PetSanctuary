﻿using PetSanctuary.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Services.Data.Catalogs
{
    public interface ICatalogService
    {
        Task Create(string name, int age, string image, string type, string gender, string cityName, string addressName, string isVaccinated, string username);

        ICollection<Pet> GetAllDogs();

        ICollection<Pet> GetAllCats();

        ICollection<Pet> GetAllUserPets(string id);

        Pet GetPetById(string id);
        Task DeletePetById(string id);
        Task EditPetById(string id, string name, int age, string image, string type, string gender, string isVaccinated, string cityName, string addressName);
    }
}
