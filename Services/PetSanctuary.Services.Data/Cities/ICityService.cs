namespace PetSanctuary.Services.Data.Cities
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICityService
    {
        CityServiceModel GetCityByName(string name);

        IEnumerable<CityServiceModel> GetAll();

        CityServiceModel GetCityById(int id);

        Task CreateAsync(string name);
    }
}
