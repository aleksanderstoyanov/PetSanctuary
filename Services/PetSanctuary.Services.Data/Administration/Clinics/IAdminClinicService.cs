namespace PetSanctuary.Services.Data.Administration.Clinics
{
    using System.Threading.Tasks;

    public interface IAdminClinicService
    {
        Task CreateAsync(string name, string addressName, string cityName, string image);

        Task EditAsync(int id, string name, string addressName, string cityName, string image);

        Task DeleteAsync(int id);
    }
}
