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
            await dbContext.Vets.AddAsync(new Vet
            {
                FirstName = "Simeon",
                Surname = "Pachev",
                ClinicId = clinicService.GetClinicByName("Provet Clinic").Id,
                Qualification = "Stomach surgeon",
                Description = "Dr. Simeon Pachev was born in Plovdiv where he completed his primary and secondary education. In 1986 he graduated as a veterinarian at VIZVM - Stara Zagora."

            });

            await dbContext.Vets.AddAsync(new Vet
            {
                FirstName = "Nikolay",
                Surname = "Marinkov",
                ClinicId = clinicService.GetClinicByName("Provet Clinic").Id,
                Qualification = "Surgeon",
                Description = @"
Dr.Nikolay Marinkov was born on March 31,
                1971 in Plovdiv.He graduated from the Veterinary College in Stara Zagora and the Thracian University in Stara Zagora.He graduated in 1998 with a master's degree in veterinary medicine."

            });
            await dbContext.Vets.AddAsync(new Vet
            {
                FirstName = "Stoyanka",
                Surname = "Karpacheva",
                ClinicId = clinicService.GetClinicByName("Provet Clinic").Id,
                Qualification = "Surgeon",
                Description = @"Dr. Stoyanka Karpacheva was born on October 7, 1963. in the city of Plovdiv. He completed his secondary education at a language high school teaching German Bertolt Brecht in Pazardzhik, and in 1987 he graduated as a veterinarian at VIZVM - Stara Zagora."
            });
            await dbContext.Vets.AddAsync(new Vet
            {
                FirstName = "Ivelina",
                Surname = "Nedkova",
                ClinicId = clinicService.GetClinicByName("Vita Vet").Id,
                Qualification = "Surgeon",
                Description = @"Dr. Ivelina Nedkova was born in 1984. He completed his secondary education at the Technical School of Veterinary Medicine in Dobrich. He graduated from the Thracian University, Stara Zagora."
            });
            await dbContext.Vets.AddAsync(new Vet
            {
                FirstName = "Genadi",
                Surname = "Stoichkov",
                ClinicId = clinicService.GetClinicByName("Vita Vet").Id,
                Qualification = "Surgeon",
                Description = @"Dr. Gennady Stoichkov was born in 1983. He graduated from the College of Veterinary Medicine in Kostinbrod in 2001. In 2007 he graduated from the Thracian University in Stara Zagora."
            });
            await dbContext.Vets.AddAsync(new Vet
            {
                FirstName = "Violeta",
                Surname = "Dimitrova",
                ClinicId = clinicService.GetClinicByName("Vita Vet").Id,
                Qualification = "Surgeon",
                Description = @"Violeta Dimitrova was born in 1994. He graduated from the College of Veterinary Medicine in Kostinbrod in 2013. He continues his studies in the specialty at the Thracian University in Stara Zagora.
"
            });
            await dbContext.Vets.AddAsync(new Vet
            {
                FirstName = "Yanitsa",
                Surname = "Dencheva",
                ClinicId = clinicService.GetClinicByName("Central Veterinary Clinic").Id,
                Qualification = "Surgeon",
                Description = @"Dr. Yanitsa Dencheva was born on August 20, 1974. In the team of the clinic since its inception,"
            });
            await dbContext.Vets.AddAsync(new Vet
            {
                FirstName = "Vladislav",
                Surname = "Zlatinov",
                ClinicId = clinicService.GetClinicByName("Central Veterinary Clinic").Id,
                Qualification = "Surgeon",
                Description = "He graduated from the National High School of Natural Sciences and Mathematics in Sofia in 1999. majoring in Biology and the University of Forestry - Sofia in 2005 as a veterinarian.,"
            });
            await dbContext.Vets.AddAsync(new Vet
            {
                FirstName = "Stoyan",
                Surname = "Nikolov",
                ClinicId = clinicService.GetClinicByName("Central Veterinary Clinic").Id,
                Qualification = "Surgeon",
                Description = "For the period 2000 - 2015 Dr. Nikolov has been to numerous internships, specializations and seminars in the Czech Republic, USA and Italy with a main focus on Anesthesiology, Cardiology, Surgery and Internal Medicine in Dogs and Cats, Diseases and Therapy in Exotic Animals, Nutrition, Breeding, etc."
            });
        }
    }
}
