using PetSanctuary.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetSanctuary.Services.Data.Vets
{
    public interface IVetService
    {
        ICollection<Vet> GetVetsById(int clinicId);

        Vet GetVetById(string id);
    }
}
