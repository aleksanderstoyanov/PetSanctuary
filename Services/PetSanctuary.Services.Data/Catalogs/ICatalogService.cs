namespace PetSanctuary.Services.Data.Catalogs
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface ICatalogService
    {
        Task Create(string name, int? age, IFormFile image, string type, string gender, string cityName, string addressName, string isVaccinated, string username, string rootPath);

        IEnumerable<CatalogServiceModel> GetPetsByType(int currentPage, int postsPerPage, string type);

        IEnumerable<CatalogServiceModel> GetAllPets();

        IEnumerable<CatalogServiceModel> GetAllUserPets(string id, int currentPage, int postPerPage, bool isAdmin);

        CatalogServiceModel GetPetById(string id);

        Task DeletePetById(string id, string rootPath);

        Task EditPetById(string id, string name, int age, IFormFile image, string type, string gender, string isVaccinated, string cityName, string addressName, string rootPath);
    }
}
