namespace PetSanctuary.Services.Data.Addresses
{
    using System.Threading.Tasks;

    public interface IAddressService
    {
        AddressServiceModel GetAddressByName(string name);

        AddressServiceModel GetAddressById(int id);

        Task CreateAsync(string name, int cityId);
    }
}
