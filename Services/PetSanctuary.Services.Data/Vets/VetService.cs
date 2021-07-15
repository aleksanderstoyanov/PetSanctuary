using PetSanctuary.Data.Common.Repositories;
using PetSanctuary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetSanctuary.Services.Data.Vets
{
    public class VetService : IVetService
    {
        private readonly IDeletableEntityRepository<Vet> vetsRepository;

        public VetService(IDeletableEntityRepository<Vet> vetsRepository)
        {
            this.vetsRepository = vetsRepository;
        }

        public Vet GetVetById(string id)
        {
            return this.vetsRepository.AllAsNoTracking().Where(x => x.Id == id).FirstOrDefault();
        }

        public ICollection<Vet> GetVetsById(int clinicId)
        {
            return this.vetsRepository.AllAsNoTracking().Where(x => x.ClinicId == clinicId).ToList();
        }
    }
}
