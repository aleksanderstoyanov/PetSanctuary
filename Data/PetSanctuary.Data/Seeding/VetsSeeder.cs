using PetSanctuary.Data.Models;
using PetSanctuary.Services.Data.Clinics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Data.Seeding
{
    public class VetsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Vets.Any())
            {
                return;
            }
            var clinicService = serviceProvider.GetService(typeof(IClinicService)) as IClinicService;
            await dbContext.Vets.AddAsync(new Vet
            {
                FirstName = "Borislav",
                Surname = "Georgiev",
                ClinicId = clinicService.GetClinicByName("SinKrust").Id,
                Qualification = "Orthopedy",
                Description = "In 2009 in Dortmund, Germany, graduated as an expert in the field of hip dysplasia, passing an exam before leading specialists from GRSK / German Association of Radiologists for Genetic Diseases of the Musculoskeletal System /."

            });
            await dbContext.Vets.AddAsync(new Vet
            {
                FirstName = "Miroslav",
                Surname = "Todorov",
                ClinicId = clinicService.GetClinicByName("SinKrust").Id,
                Qualification = "Neurology",
                Description = @"Dr. Todorov was born on December 2, 1985. in the town of Dobrich. He is finishing his high school
education in Dobrich in Professional High School of Veterinary Medicine.
In 2010 he graduated from the University of Forestry,
                Sofia - specialty
Veterinary Medicine.
Since 2006 is part of the team of Veterinary Clinic Son
Cross"
            });

            await dbContext.Vets.AddAsync(new Vet
            {
                FirstName = "Ana",
                Surname = "Gospodinova",
                ClinicId = clinicService.GetClinicByName("SinKrust").Id,
                Qualification = "Dermathology",
                Description = @"Dr. Gospodinova was born on August 23, 1984. town of Kardzhali.
He graduated in veterinary medicine in 2009 at the University of Forestry in Sofia.
He started his internship at the Blue Cross Veterinary Clinic in 2006."

            });
            await dbContext.Vets.AddAsync(new Vet
            {
                FirstName = "Ivan",
                Surname = "Dochev",
                ClinicId = clinicService.GetClinicByName("Izgrev").Id,
                Qualification = "Surgeon",
                Description = @"Dr. Dochev graduated in veterinary medicine in 2001 at the Thracian University in Stara Zagora."

            });
            await dbContext.Vets.AddAsync(new Vet
            {
                Surname = "Docheva",
                ClinicId = clinicService.GetClinicByName("Izgrev").Id,
                Qualification = "Dermathology",
                Description = @"Dr. Docheva completed her education in veterinary medicine in 2012 at the University of Veterinary Medicine Vienna - Austria, module pets and exotic animals."

            });
            await dbContext.Vets.AddAsync(new Vet
            {
                Surname = "Stefanov",
                ClinicId = clinicService.GetClinicByName("Izgrev").Id,
                Qualification = "Dermathology",
                Description = @"Dr. Stefanov graduated in veterinary medicine in 2003 at the Thracian University in Stara Zagora."

            });
            await dbContext.Vets.AddAsync(new Vet
            {
                FirstName = "Nikolay",
                Surname = "Kolev",
                ClinicId = clinicService.GetClinicByName("Diana").Id,
                Qualification = "Dermathology",
                Description = @"Born on January 6, 1967 in the village of Hadjidimitrovo, municipality. Svishtov. He graduated from the Technical School of Veterinary Medicine in Lovech in 1986. Higher education, specialty Veterinary Medicine received from the Thracian University"

            });
            await dbContext.Vets.AddAsync(new Vet
            {
                FirstName = "Margarita",
                Surname = "Kuncheva",
                ClinicId = clinicService.GetClinicByName("Diana").Id,
                Qualification = "Dermathology",
                Description = @"Born on February 27, 1979 in the town of Shumen.Graduated from a language high school in 1998 in the town of Shumen. He received his higher veterinary medical education at the Thracian University in Stara Zagora in 2004. Since the same year he has been a veterinarian in Diana's office. He is interested in cat diseases and orthopedics."

            });
            await dbContext.Vets.AddAsync(new Vet
            {
                FirstName = "Pavlin",
                Surname = "Todorov",
                ClinicId = clinicService.GetClinicByName("Diana").Id,
                Qualification = "Dermathology",
                Description = @"Born in 1985 in the town of Svishtov. In 2004 he graduated from the Technical School of Veterinary Medicine - Lovech. He received his higher education at the Faculty of Veterinary Medicine of the Thracian University - Stara Zagora in 2009."

            });

        }
    }
}
