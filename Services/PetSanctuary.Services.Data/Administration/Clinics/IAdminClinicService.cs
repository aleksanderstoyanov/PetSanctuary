using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Services.Data.Administration.Clinics
{
    public interface IAdminClinicService
    {
        Task Create(string name, string addressName, string cityName, string image);

        Task Edit(int id, string name, string addressName, string cityName, string image);

        Task Delete(int id);
    }
}
