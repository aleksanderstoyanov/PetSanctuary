using PetSanctuary.Data.Models;
using PetSanctuary.Web.ViewModels.Catalog;
using System.Collections.Generic;
using MyTested.AspNetCore.Mvc;
using System.Linq;
using PetSanctuary.Data.Models.Enums;

namespace PetSanctuary.Test.Data
{
    public class PetTestData
    {
        public static List<Pet> GetPets(int count, PetType type)
        {
            var user = new ApplicationUser
            {
                Id = TestUser.Identifier,
                UserName = TestUser.Username
            };

            var pets = Enumerable
                .Range(1, count)
                .Select(i => new Pet
                {
                    Id = "PetId" + i,
                    Name = "Test" + i,
                    Image = "TestImage"+i,
                    Type = type,
                    City = new City
                    {
                        Id = i,
                        Name = "TestCity"+i
                    },
                    Address = new Address
                    {
                        Id = i,
                        Name = "TestAddress"+i,
                        CityId = i
                    },
                    Age = i,
                    Gender = GenderType.Male,
                    Owner = user,
                    IsVaccinated=true
                }).ToList();

            return pets;
        }
    }
}
