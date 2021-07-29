using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Services.Data.Administration.Clinics
{
    public interface IAdminClinicService
    {
        Task CreateAsync(string name, string addressName, string cityName, string image);

        Task EditAsync(int id, string name, string addressName, string cityName, string image);

        Task DeleteAsync(int id);
    }
}
