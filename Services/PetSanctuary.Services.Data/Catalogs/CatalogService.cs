using PetSanctuary.Data.Common.Repositories;
using PetSanctuary.Data.Models;
using PetSanctuary.Data.Models.Enums;
using PetSanctuary.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Services.Data.Catalogs
{
    public class CatalogService : ICatalogService
    {
        private readonly IRepository<Pet> petsRepository;

        public CatalogService(IRepository<Pet> petsRepository)
        {
            this.petsRepository = petsRepository;
        }

        public Task Create(string name, int age, string image, string type, string city, string address, string isVaccinated)
        {
            throw new NotImplementedException();
        }

        public ICollection<Pet> GetAllCats()
        {
            return this.petsRepository.All().Where(x => x.Type.ToString() == "Cats").ToList();
        }

        public ICollection<Pet> GetAllDogs()
        {
            return this.petsRepository.All().Where(x => x.Type.ToString() == "Dogs").ToList();
        }
    }
}
