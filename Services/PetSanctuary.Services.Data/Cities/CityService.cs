using PetSanctuary.Data.Common.Models;
using PetSanctuary.Data.Common.Repositories;
using PetSanctuary.Data.Models;
using PetSanctuary.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Services.Data.Cities
{
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
                CreatedOn = DateTime.UtcNow

            });

            await this.citiesRepository.SaveChangesAsync();
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
