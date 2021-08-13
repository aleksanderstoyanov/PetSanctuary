namespace PetSanctuary.Services.Data.Vets
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IVetService
    {
        IEnumerable<VetServiceModel> GetVetsById(int clinicId);

        Task UpdateLikesAsync(string vetId, string userId);

        Task UpdateDislikesAsync(string vetId, string userId);

        VetServiceModel GetVetById(string id);
    }
}
