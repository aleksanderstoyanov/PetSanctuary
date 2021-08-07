using PetSanctuary.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace PetSanctuary.Test.Data
{
    public class VetsTestData
    {
        public static List<Vet> GetVets(int count)
        {
            var clinic = new Clinic
            {
                Id = 1,
                Name = "Test",
                City = new City
                {
                    Name = "TestCity"
                },
                Address = new Address
                {
                    Name = "TestAddress",
                    City = new City
                    {
                        Name = "TestCity"
                    }
                },
                Image = "TestImage"

            };

            var vets = Enumerable
                .Range(1, count)
                .Select(i => new Vet
                {
                    Id="TestId"+i,
                    FirstName = "FirstName" + i,
                    Surname = "Surname" + i,
                    Qualification = "Veterinary",
                    Description = "Test description" + i,
                    Clinic = clinic
                })
                .ToList();

            return vets;
        }
    }
}
