namespace PetSanctuary.Test.Pipeline
{
    using PetSanctuary.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using PetSanctuary.Test.Data;
    using System.Collections.Generic;
    using PetSanctuary.Web.ViewModels.Vets;
    using System.Linq;

    public class VetsPipelineTest
    {
        [Fact]
        public void GetIndexShouldReturnViewWithValidModel()
            => MyMvc
             .Pipeline()
            .ShouldMap("/Vets/Index/1")
             .To<VetsController>(c => c.Index(1))
            .Which(controller => controller
                .WithData(VetsTestData.GetVets(3)))
            .ShouldReturn()
              .View(result => result.WithModelOfType<List<VetsByIdViewModel>>()
                 .Passing(model => model.Count() == 3));

        [Fact]
        public void GetLikeShouldBeForAuthorizedUsersAndShouldReturnDefaultRedirectToAction()
            => MyMvc
            .Pipeline()
            .ShouldMap(request => request
                  .WithMethod(HttpMethod.Get)
                  .WithLocation("/Vets/Like/TestId1")
                  .WithUser()
                  .WithAntiForgeryToken())
              .To<VetsController>(c => c.Like("TestId1"))
            .Which(controller => controller
                 .WithUser()
                 .WithData(VetsTestData.GetVets(1)))
            .ShouldHave()
              .ActionAttributes(attributes => attributes
                  .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
             .RedirectToAction("Index", "Vets", new { id = 1 });

        [Fact]
        public void GetDislikeShouldBeForAuthorizedUsersAndShouldReturnDefaultRedirectToAction()
             =>MyMvc
            .Pipeline()
              .ShouldMap(request => request
                  .WithMethod(HttpMethod.Get)
                  .WithLocation("/Vets/Dislike/TestId1")
                  .WithUser()
                  .WithAntiForgeryToken())
              .To<VetsController>(c => c.Dislike("TestId1"))
            .Which(controller => controller
                 .WithUser()
                 .WithData(VetsTestData.GetVets(1)))
            .ShouldHave()
              .ActionAttributes(attributes => attributes
                  .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
             .RedirectToAction("Index", "Vets", new { id = 1 });


        [Fact]
        public void GetDetailsShouldReturnDefaultViewWithValidModel()
            => MyMvc
            .Pipeline()
              .ShouldMap("/Vets/Description/TestId1")
            .To<VetsController>(c => c.Description("TestId1"))
              .Which(controller => controller
                  .WithData(VetsTestData.GetVets(1)))
            .ShouldReturn()
            .View(result => result
               .WithModelOfType<VetsDetailViewModel>()
               .Passing(model=>model.FirstName=="FirstName1"));
    }
}
