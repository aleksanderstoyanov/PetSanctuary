namespace PetSanctuary.Test.Controllers
{
    using PetSanctuary.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using PetSanctuary.Data.Models;
    using PetSanctuary.Data.Models.Enums;
    using System;
    using System.Collections.Generic;
    using PetSanctuary.Web.ViewModels.Catalog;
    using PetSanctuary.Test.Data;

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
            


    }
}
