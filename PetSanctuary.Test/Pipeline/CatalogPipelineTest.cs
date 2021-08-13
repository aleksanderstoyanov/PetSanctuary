namespace PetSanctuary.Test.Pipeline
{
    using PetSanctuary.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using PetSanctuary.Web.ViewModels.Catalog;
    using PetSanctuary.Test.Data;
    using PetSanctuary.Data.Models.Enums;
    using PetSanctuary.Data.Models;
    using System.Linq;

    public class CatalogPipelineTest
    {

        [Theory]
        [InlineData(3, PetType.Dog)]
        [InlineData(3, PetType.Cat)]
        [InlineData(3, PetType.Other)]
        public void GetIndexShouldReturnDefaultViewWithValidModel(int count, PetType type)
            => MyPipeline
            .Configuration()
             .ShouldMap(request => request
                 .WithPath("/Catalog")
                 .WithQuery("Type", type.ToString()))
            .To<CatalogController>(c => c.Index(new CatalogQueryModel { Type = type.ToString() }))
            .Which(controller => controller
               .WithUser()
               .WithData(PetTestData.GetPets(count, type)))
            .ShouldReturn()
              .View(result => result
                 .WithModelOfType<CatalogQueryModel>()
                 .Passing(model => model.Pets.Count == 3));

        [Fact]
        public void GetCreateShouldReturnDefaultView()
             => MyPipeline
            .Configuration()
              .ShouldMap(request => request
                 .WithMethod(HttpMethod.Get)
                 .WithLocation("/Catalog/Create")
                 .WithUser()
                 .WithAntiForgeryToken())
            .To<CatalogController>(c => c
              .Create())
            .Which(controller => controller
               .WithUser())
            .ShouldHave()
             .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View();

        [Theory]
        [InlineData(1, PetType.Dog)]
        [InlineData(1, PetType.Cat)]
        [InlineData(1, PetType.Other)]
        public void GetEditShouldReturnDefaultView(int count, PetType type)
            => MyPipeline
            .Configuration()
            .ShouldMap(request => request
               .WithPath("/Catalog/Edit/PetId1")
               .WithUser())
            .To<CatalogController>(c => c
              .Edit("PetId1"))
            .Which(controller => controller
              .WithData(PetTestData.GetPets(count, type)))
            .ShouldHave()
             .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View(result => result
               .WithModelOfType<CatalogEditFormModel>());

        [Theory]
        [InlineData(1, PetType.Dog, "EditedName", 0, "TestAddress", "TestCity", "Dog", "Male", "Yes")]
        [InlineData(1, PetType.Cat, "EditedName", 0, "TestAddress", "TestCity", "Dog", "Male", "Yes")]
        [InlineData(1, PetType.Other, "EditedName", 0, "TestAddress", "TestCity", "Dog", "Male", "Yes")]

        public void PostEditShouldReturnDefaultRedirectToActionAndShouldBeForAuthorizedUsers
            (int count, PetType type, string name, int age, string address, string city, string model, string gender, string isVaccinated)
            => MyPipeline
            .Configuration()
            .ShouldMap(request => request
               .WithMethod(HttpMethod.Post)
               .WithPath("/Catalog/Edit/PetId1")
               .WithFormFields(new
               {
                   Id = "PetId1",
                   Name = name,
                   Age = age,
                   Type = model,
                   Gender = gender,
                   isVaccinated = isVaccinated,
                   City = city,
                   Address = address
               })
                .WithUser()
                .WithAntiForgeryToken())
            .To<CatalogController>(c => c.Edit("PetId1", new CatalogEditFormModel
            {
                Id = "PetId1",
                Name = name,
                Age = age,
                Type = model,
                Gender = gender,
                IsVaccinated = isVaccinated,
                City = city,
                Address = address
            }))
            .Which(controller => controller
              .WithData(PetTestData.GetPets(count, type)))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
              .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldHave()
            .Data(data => data
               .WithSet<Pet>(model => model
                  .Any(pet => pet.Name == name)))
            .AndAlso()
            .ShouldReturn()
            .RedirectToAction("Posts", "MyProfile");

        [Theory]
        [InlineData(1, PetType.Dog)]
        [InlineData(1, PetType.Cat)]
        [InlineData(1, PetType.Other)]
        public void GetDeleteShouldRedirectToDefaultActionAndShouldBeForAuthorizedUsers(int count, PetType type)
            => MyPipeline
            .Configuration()
            .ShouldMap(request => request
               .WithPath("/Catalog/Delete/PetId1")
               .WithUser())
            .To<CatalogController>(c => c.Delete("PetId1"))
            .Which(controller => controller
               .WithData(PetTestData.GetPets(count, type)))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
               .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldHave()
            .Data(data => data
               .WithSet<Pet>(model => !model.Any()))
            .AndAlso()
            .ShouldReturn()
            .RedirectToAction("Posts", "MyProfile");


    }
}
