using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Services.Data.Administration.Vets
{
    public interface IAdminVetService
    {
        Task Create(string firstName, string surname, string description, string qualification, string clinic);

        Task Edit(string id, string firstName, string surname, string description, string qualification, string clinic);

        Task Delete(string id);
    }
}
