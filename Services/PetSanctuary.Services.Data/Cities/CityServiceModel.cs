namespace PetSanctuary.Services.Data.Cities
{
    using AutoMapper;
    using PetSanctuary.Data.Models;
    using PetSanctuary.Services.Mapping;

    public class CityServiceModel : IMapFrom<City>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<City, CityServiceModel>();
        }
    }
}
