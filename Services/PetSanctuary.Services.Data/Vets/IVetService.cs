namespace PetSanctuary.Services.Data.Vets
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IVetService
    {
        IEnumerable<VetServiceModel> GetVetsById(int clinicId);

        Task UpdateLikesAsync(string vetId);

        Task UpdateDislikesAsync(string vetId);

        VetServiceModel GetVetById(string id);
    }
}
