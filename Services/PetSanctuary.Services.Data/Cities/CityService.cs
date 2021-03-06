namespace PetSanctuary.Services.Data.Cities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PetSanctuary.Data.Common.Repositories;
    using PetSanctuary.Data.Models;
    using PetSanctuary.Services.Mapping;

    public class CityService : ICityService
    {
        private readonly IDeletableEntityRepository<City> citiesRepository;

        public CityService(IDeletableEntityRepository<City> citiesRepository)
        {
            this.citiesRepository = citiesRepository;
        }

        public async Task CreateAsync(string name)
        {
            await this.citiesRepository.AddAsync(new City
            {
                Name = name,
                CreatedOn = DateTime.UtcNow,
            });

            await this.citiesRepository.SaveChangesAsync();
        }

        public IEnumerable<CityServiceModel> GetAll()
        {
            return this.citiesRepository
                .AllAsNoTracking()
                .To<CityServiceModel>();
        }

        public CityServiceModel GetCityById(int id)
        {
            return this.citiesRepository
              .AllAsNoTracking()
              .Where(city => city.Id == id)
              .To<CityServiceModel>()
              .FirstOrDefault();
        }

        public CityServiceModel GetCityByName(string name)
        {
            return this.citiesRepository
              .All()
              .Where(city => city.Name == name)
              .To<CityServiceModel>()
              .FirstOrDefault();
        }
    }
}
