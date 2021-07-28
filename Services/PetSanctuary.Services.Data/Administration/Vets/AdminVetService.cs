﻿using PetSanctuary.Data.Common.Repositories;
using PetSanctuary.Data.Models;
using PetSanctuary.Services.Data.Clinics;
using PetSanctuary.Services.Data.Vets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Services.Data.Administration.Vets
{
    public class AdminVetService : IAdminVetService
    {
        private readonly IDeletableEntityRepository<Vet> vetRepository;
        private readonly IClinicService clinicService;

        public AdminVetService(IDeletableEntityRepository<Vet> vetRepository, IClinicService clinicService)
        {
            this.vetRepository = vetRepository;
            this.clinicService = clinicService;
        }

        public async Task Create(string firstName, string surname, string description, string qualification, string clinic)
        {
            await this.vetRepository.AddAsync(new Vet
            {
                FirstName = firstName,
                Surname = surname,
                Description = description,
                Qualification = qualification,
                ClinicId = this.clinicService.GetClinicByName(clinic).Id
            });
            await this.vetRepository.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            this.vetRepository.Delete(this.vetRepository
                .All()
                .FirstOrDefault(vet => vet.Id == id));

            await this.vetRepository.SaveChangesAsync();
        }

        public async Task Edit(string id, string firstName, string surname, string description, string qualification, string clinic)
        {
            var vet = this.vetRepository
                .All()
                .FirstOrDefault(vet => vet.Id == id);

            vet.FirstName = firstName;
            vet.Surname = surname;
            vet.Description = description;
            vet.Qualification = qualification;
            vet.ClinicId = this.clinicService.GetClinicByName(clinic).Id;

            await this.vetRepository.SaveChangesAsync();
        }
    }
}