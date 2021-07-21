﻿using PetSanctuary.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Services.Data.Vets
{
    public interface IVetService
    {
        IEnumerable<VetServiceModel> GetVetsById(int clinicId);

        Task UpdateLikes(string vetId);

        Task UpdateDislikes(string vetId);

        VetServiceModel GetVetById(string id);
    }
}
