using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Services.Data.Administration.Vets
{
    public interface IAdminVetService
    {
        Task CreateAsync(string firstName, string surname, string description, string qualification, string clinic);

        Task EditAsync(string id, string firstName, string surname, string description, string qualification, string clinic);

        Task DeleteAsync(string id);
    }
}
