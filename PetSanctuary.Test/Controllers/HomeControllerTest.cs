namespace PetSanctuary.Test.Controllers
{
    using PetSanctuary.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using PetSanctuary.Data.Models;
    using System;
    using PetSanctuary.Data.Models.Enums;
    using PetSanctuary.Web.ViewModels.Home;
    using System.Collections.Generic;
    using PetSanctuary.Web.ViewModels;

    public class HomeControllerTest
    {
        [Fact]
        public void PrivacyShouldReturnProperView()
            => MyController<HomeController>
               .Instance()
               .Calling(c => c.Privacy())
               .ShouldReturn()
               .View();

        [Fact]
        public void IndexShouldReturnProperView()
          => MyController<HomeController>
            .Instance()
            .WithData(new Pet
            {
                Id = Guid.NewGuid().ToString(),
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
            .Calling(c => c.Index())
            .ShouldReturn()
            .View(result => result
             .WithModelOfType<List<AllDogsHomeViewModel>>()
            .Passing(model => model.Count == 1));

        [Fact]
        public void ErrorShouldReturnProperView()
         => MyController<HomeController>
            .Instance()
            .Calling(c => c.Error())
            .ShouldReturn()
            .View(view => view
            .WithModelOfType<ErrorViewModel>());
           
    }
}
