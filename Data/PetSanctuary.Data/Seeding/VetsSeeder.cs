using PetSanctuary.Data.Models;
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

            await dbContext.Vets.AddAsync(new Vet
            {
                FirstName = "Borislav",
                Surname = "Georgiev",
                ClinicId = 5,
                Qualification = "Orthopedy",
                Description = "In 2009 in Dortmund, Germany, graduated as an expert in the field of hip dysplasia, passing an exam before leading specialists from GRSK / German Association of Radiologists for Genetic Diseases of the Musculoskeletal System /."

            });
            await dbContext.Vets.AddAsync(new Vet
            {
                FirstName = "Miroslav",
                Surname = "Todorov",
                ClinicId = 5,
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
                ClinicId = 5,
                Qualification = "Dermathology",
                Description = @"Dr. Gospodinova was born on August 23, 1984. town of Kardzhali.
He graduated in veterinary medicine in 2009 at the University of Forestry in Sofia.
He started his internship at the Blue Cross Veterinary Clinic in 2006."

            });
        }
    }
}
