namespace PetSanctuary.Test.Controllers
{
    using PetSanctuary.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using PetSanctuary.Data.Models.Enums;
    using System.Collections.Generic;
    using PetSanctuary.Web.ViewModels.Catalog;
    using PetSanctuary.Test.Data;
    using Microsoft.AspNetCore.Http;
    using System.IO;
    using System.Linq;
    using PetSanctuary.Data.Models;

    public class CatalogControllerTest
    {
        [Fact]
        public void DogsShouldReturnProperView()
          => MyController<CatalogController>
            .Instance()
            .WithData(PetTestData.GetPets(10, PetType.Dog))
            .Calling(c => c.Dogs())
            .ShouldReturn()
            .View(result => result
            .WithModelOfType<List<CatalogViewModel>>()
            .Passing(model => model.Count == 10));

        [Fact]
        public void CatsShouldReturnProperViewModel()
        {
            MyController<CatalogController>
            .Instance()
            .WithData(PetTestData.GetPets(5, PetType.Cat))
            .Calling(c => c.Cats())
            .ShouldReturn()
            .View(result => result
            .WithModelOfType<List<CatalogViewModel>>()
            .Passing(model => model.Count == 5));
        }

        [Fact]
        public void OthersShouldReturnProperViewModel()
         => MyController<CatalogController>
            .Instance()
            .WithData(PetTestData.GetPets(7, PetType.Other))
            .Calling(c => c.Other())
            .ShouldReturn()
            .View(result => result
            .WithModelOfType<List<CatalogViewModel>>()
            .Passing(model => model.Count == 7));


        [Fact]
        public void CreateShouldReturnProperViewAndShouldBeForAuthorizedUsers()
         => MyController<CatalogController>
            .Instance()
            .Calling(c => c.Create())
            .ShouldHave()
            .ActionAttributes(attributes => attributes
            .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View();

        [Theory]
        [InlineData("TestName", "TestAddress", "TestCity", 1, "Dog", "Male", true)]
        [InlineData("TestName", "TestAddress", "TestCity", 2, "Cat", "Male", false)]
        [InlineData("TestName", "TestAddress", "TestCity", 3, "Other", "Male", false)]
        public void PostCreateShouldBeForAuthorizedUsersAndShouldRedirectWithValidModel(string name, string address, string city, int age, string type, string gender, bool isVaccinated)
            => MyController<CatalogController>
              .Instance(c => c
                .WithUser())
            .Calling(c => c.Create(new CatalogFormCreateViewModel
            {
                Name = name,
                Address = address,
                City = city,
                Age = age,
                Type = type,
                Gender = gender,
                IsVaccinated = "Yes",
                Image = new FormFile(File.OpenRead(@"C:\Users\black\OneDrive\Desktop\PetSanctuary\Web\PetSanctuary.Web\wwwroot\img\Test.png"), 0, "Test.png".Length, null, "Test.png")
            }))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
            .RestrictingForAuthorizedRequests()
            .RestrictingForHttpMethod(HttpMethod.Post))
            .AndAlso()
            .ShouldHave()
            .ValidModelState()
            .Data(data => data
               .WithSet<Pet>(pets => pets
                  .Any(pet => pet.Name == name)))
            .AndAlso()
            .ShouldReturn()
            .RedirectToAction("Dogs", "Catalog");

        [Fact]
        public void DetailsShouldReturnProperView()
          => MyController<CatalogController>
            .Instance()
            .WithData(new Pet
            {
                Id = "TestId",
                Name = "Test",
                Image = "TestImage",
                Type = PetType.Dog,
                City = new City
                {
                    Id = 1,
                    Name = "TestCity"
                },
                Address = new Address
                {
                    Id = 1,
                    Name = "TestAddress",
                    CityId = 1
                },
                Age = 1,
                Gender = GenderType.Male,
                Owner = new ApplicationUser
                {
                    Id = TestUser.Identifier,
                    UserName = TestUser.Username,

                }
            })
            .Calling(c => c.Details("TestId"))
            .ShouldReturn()
            .View();
    }
}
