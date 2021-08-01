namespace PetSanctuary.Services.Data.Administration.Vets
{
    using System.Threading.Tasks;

    public interface IAdminVetService
    {
        Task CreateAsync(string firstName, string surname, string description, string qualification, string clinic);

        Task EditAsync(string id, string firstName, string surname, string description, string qualification, string clinic);

        Task DeleteAsync(string id);
    }
}
