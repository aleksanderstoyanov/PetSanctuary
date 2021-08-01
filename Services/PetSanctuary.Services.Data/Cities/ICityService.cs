namespace PetSanctuary.Services.Data.Cities
{
    using System.Threading.Tasks;

    public interface ICityService
    {
        CityServiceModel GetCityByName(string name);

        CityServiceModel GetCityById(int id);

        Task CreateAsync(string name);
    }
}
