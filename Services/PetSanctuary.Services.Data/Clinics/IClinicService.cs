using PetSanctuary.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetSanctuary.Services.Data.Clinics
{
    public interface IClinicService
    {
        ICollection<Clinic> GetAllClinics();
    }
}
