using PetSanctuary.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace PetSanctuary.Test.Data
{
    public class ClinicsTestData
    {
        public static List<Clinic> GetClinics(int count, string city)
        {
            var clinics = Enumerable
                .Range(1, count)
                .Select(i => new Clinic
                {
                    Name = "Test" + i,
                    City = new City
                    {
                        Name = city
                    },
                    Address = new Address
                    {
                        Name = "TestAddress" + i,
                        City = new City
                        {
                            Name = "TestCity" + i
                        }
                    },
                    Image = "TestImage" + i

                }).ToList();

            return clinics;
        }
    }
}
