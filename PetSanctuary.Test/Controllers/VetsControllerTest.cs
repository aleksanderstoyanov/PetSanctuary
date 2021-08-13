namespace PetSanctuary.Test.Controllers
{
    using PetSanctuary.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using PetSanctuary.Test.Data;
    using PetSanctuary.Web.ViewModels.Vets;
    using System.Collections.Generic;
    using PetSanctuary.Data.Models;
    using System.Linq;

    public class VetsControllerTest
    {
        [Fact]
        public void IndexShouldReturnProperViewModel()
            => MyController<VetsController>
            .Instance()
            .WithData(VetsTestData
                 .GetVets(3))
            .Calling(c => c
               .Index(1))
            .ShouldReturn()
            .View(result => result
              .WithModelOfType<List<VetsByIdViewModel>>()
            .Passing(model => model.Count == 3));

        

        [Fact]
        public void DetailsShouldReturnProperView()
            => MyController<VetsController>
            .Instance()
            .WithUser()
            .WithData(VetsTestData
                .GetVets(1))
            .Calling(c => c.Description("TestId1"))
            .ShouldReturn()
            .View(result => result.WithModelOfType<VetsDetailViewModel>());
    }
}
