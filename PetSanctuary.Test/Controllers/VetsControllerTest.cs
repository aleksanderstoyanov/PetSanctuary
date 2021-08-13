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
        public void LikeShouldRedirectToProperActionAndShouldBeForAuthorizedUsers()
            => MyController<VetsController>
            .Instance()
            .WithUser()
            .WithData(VetsTestData
                .GetVets(1))
            .Calling(c => c.Like("TestId1"))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                 .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldHave()
            .Data(data => data
                .WithSet<Vet>(data => data
                  .FirstOrDefault(vet => vet.Id == "TestId1")
                  .Likes.Count() == 1))
            .AndAlso()
            .ShouldReturn()
            .RedirectToAction("Index", "Vets", new { id = 1 });
        [Fact]
        public void LikeShouldRedirectToProperActionAndShouldBeForAuthorizedUsersAndShouldRemoveLikeIfAlreadyLiked()
            => MyController<VetsController>
             .Instance()
             .WithUser()
             .WithData(VetsTestData.GetVets(1, true))
            .Calling(c => c.Like("TestId1"))
             .ShouldHave()
             .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldHave()
             .Data(data => data
               .WithSet<Like>(data => !data.Any()))
            .AndAlso()
            .ShouldReturn()
            .RedirectToAction("Index", "Vets", new { id = 1 });


        [Fact]
        public void DislikeShouldRedirectToProperActionAndShouldBeForAuthorizedUsers()
           => MyController<VetsController>
           .Instance()
           .WithUser()
           .WithData(VetsTestData
               .GetVets(1))
           .Calling(c => c.Dislike("TestId1"))
           .ShouldHave()
           .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
             .AndAlso()
            .ShouldHave()
            .Data(data => data
                .WithSet<Vet>(data => data
                  .FirstOrDefault(vet => vet.Id == "TestId1")
                  .Dislikes.Count() == 1))
           .AndAlso()
           .ShouldReturn()
           .RedirectToAction("Index", "Vets", new { id = 1 });

        [Fact]
        public void DislikeShouldRedirectToProperActionAndShouldBeForAuthorizedUsersAndShouldRemoveDislikeIfAlreadyDisliked()
           => MyController<VetsController>
            .Instance()
            .WithUser()
            .WithData(VetsTestData.GetVets(1, true))
           .Calling(c => c.Dislike("TestId1"))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
               .RestrictingForAuthorizedRequests())
           .AndAlso()
           .ShouldHave()
            .Data(data => data
              .WithSet<Dislike>(data => !data.Any()))
           .AndAlso()
           .ShouldReturn()
           .RedirectToAction("Index", "Vets", new { id = 1 });

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
