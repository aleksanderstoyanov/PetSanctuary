namespace PetSanctuary.Test.Routing.Admin
{
    using PetSanctuary.Web.Areas.Administration.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using PetSanctuary.Web.ViewModels.Administration.Vets;

    public class VetsControllerTest
    {
        [Fact]
        public void GetCreateShouldBeMappedCorrectly()
             => MyRouting
              .Configuration()
               .ShouldMap(request => request
                  .WithPath("/Administration/Vets/Create")
                  .WithMethod(HttpMethod.Get))
            .To<VetsController>(c => c
                .Create());

        [Fact]
        public void PostCreateShouldBeMappedCorrectly()
              => MyRouting
                .Configuration()
                 .ShouldMap(request => request
                   .WithPath("/Administration/Vets/Create/TestId")
                   .WithMethod(HttpMethod.Post))
            .To<VetsController>(c => c
                .Create(new VetInputModel()));

        [Fact]
        public void GetEditShouldBeMappedCorrectly()
              => MyRouting
               .Configuration()
                 .ShouldMap(request => request
                     .WithPath("/Administration/Vets/Edit/TestId"))
            .To<VetsController>(c => c
              .Edit("TestId"));

        [Fact]
        public void PostEditShouldBeMappedCorrectly()
             => MyRouting
              .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Administration/Vets/Edit/TestId")
                    .WithMethod(HttpMethod.Post)
                    .WithQuery("clinicId", "1"))
            .To<VetsController>(c => c
                .Edit(1, "TestId", new VetInputModel()));

        [Fact]
        public void DeleteShouldBeMappedCorrectly()
             => MyRouting
             .Configuration()
               .ShouldMap(request => request
                  .WithPath("/Administration/Vets/Delete/TestId")
                  .WithQuery("clinicId", "1"))
            .To<VetsController>(c => c
               .Delete(1, "TestId"));

    }
}
