namespace PetSanctuary.Services.Data.Catalogs
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICatalogService
    {
        Task Create(string name, int age, string image, string type, string gender, string cityName, string addressName, string isVaccinated, string username);

        IEnumerable<CatalogServiceModel> GetAllPets();

        IEnumerable<CatalogServiceModel> GetAllDogs();

        IEnumerable<CatalogServiceModel> GetAllCats();

        IEnumerable<CatalogServiceModel> GetAllOthers();

        IEnumerable<CatalogServiceModel> GetAllUserPets(string id, int currentPage, int postPerPage, bool isAdmin);

        CatalogServiceModel GetPetById(string id);

        Task DeletePetById(string id);

        Task EditPetById(string id, string name, int age, string image, string type, string gender, string isVaccinated, string cityName, string addressName);
    }
}
