namespace PetSanctuary.Test.Controllers
{
    using PetSanctuary.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using PetSanctuary.Data.Models.Enums;
    using PetSanctuary.Web.ViewModels.Catalog;
    using PetSanctuary.Test.Data;
    using Microsoft.AspNetCore.Http;
    using System.IO;
    using System.Linq;
    using PetSanctuary.Data.Models;

    public class CatalogControllerTest
    {
        [Theory]
        [InlineData(3, PetType.Dog)]
        [InlineData(3, PetType.Cat)]
        [InlineData(3, PetType.Other)]
        public void IndexShouldReturnProperView(int count, PetType type)
            => MyController<CatalogController>
            .Instance()
            .WithHttpRequest(request => request
              .WithQuery("Type", type.ToString()))
            .WithData(PetTestData.GetPets(count, type))
              .Calling(c => c
                .Index(new CatalogQueryModel { Type = type.ToString() }))
            .ShouldReturn()
            .View(result => result
               .WithModelOfType<CatalogQueryModel>());

        [Fact]
        public void GetCreateShouldReturnProperView()
            => MyController<CatalogController>
            .Instance()
             .Calling(c => c
               .Create())
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
            .TempData(tempData => tempData
            .ContainingEntryWithKey("message"))
            .Data(data => data
               .WithSet<Pet>(pets => pets
                  .Any(pet => pet.Name == name)))
            .AndAlso()
            .ShouldReturn()
            .RedirectToAction("Index", "Catalog");


        [Fact]
        public void GetEditShouldBeForAuthorizedUsersAndShouldReturnView()
             => MyController<CatalogController>
               .Instance()
                 .WithUser()
                 .WithData(PetTestData.GetPets(1, PetType.Dog))
            .Calling(c => c.Edit("PetId1"))
              .ShouldHave()
              .ActionAttributes(attributes => attributes
                  .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View();

        [Fact]
        public void PostEditShouldBeForAuthorizedUsersAndShouldRedirectToProperActionWithValidModel()
            => MyController<CatalogController>
             .Instance()
               .WithUser()
               .WithData(PetTestData.GetPets(1, PetType.Dog))
            .Calling(c => c.Edit("PetId1", new CatalogEditFormModel
            {
                Name = "Sharo",
                Type = "Dog",
                City = "TestCity1",
                Address = "TestAddress1",
                Gender = "Male",
                Age = 2,
                IsVaccinated = "Yes"
            }))
            .ShouldHave()
              .ActionAttributes(attributes => attributes
                  .RestrictingForAuthorizedRequests()
                  .RestrictingForHttpMethod(HttpMethod.Post))
            .AndAlso()
            .ShouldHave()
             .TempData(tempData => tempData
             .ContainingEntryWithKey("message"))
             .Data(data => data.WithSet<Pet>(data => data
                     .Any(pet => pet.Name == "Sharo")))
            .AndAlso()
            .ShouldReturn()
            .RedirectToAction("Posts", "MyProfile");


        [Fact]
        public void DeleteShouldBeForAuthorizedAndShouldRedirectToProperAction()
             => MyController<CatalogController>
            .Instance()
               .WithUser()
               .WithData(PetTestData.GetPets(1, PetType.Dog))
            .Calling(c => c.Delete("PetId1"))
            .ShouldHave()
              .ActionAttributes(attributes => attributes
                  .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldHave()
            .TempData(tempData => tempData
            .ContainingEntryWithKey("message"))
            .Data(data => data
                 .WithSet<Pet>(data => data
                    .Count() == 0))
            .AndAlso()
            .ShouldReturn()
            .RedirectToAction("Posts", "MyProfile");

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
