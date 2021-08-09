namespace PetSanctuary.Test.Routing.Admin
{
    using PetSanctuary.Web.Areas.Administration.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using PetSanctuary.Web.ViewModels.Administration.Clinics;

    public class ClinicsControllerTest
    {
        [Fact]
        public void GetCreateShouldBeMappedCorrectly()
             => MyRouting
            .Configuration()
            .ShouldMap(request => request
                .WithMethod(HttpMethod.Get)
                .WithPath("/Administration/Clinics/Create"))
            .To<ClinicsController>(c => c
                .Create());

        [Fact]
        public void PostCreateShouldBeMappedCorrectly()
            => MyRouting
             .Configuration()
             .ShouldMap(request => request
                  .WithMethod(HttpMethod.Post)
                  .WithPath("/Administration/Clinics/Create"))
            .To<ClinicsController>(c => c
                 .Create(new ClinicInputModel()));

        [Fact]
        public void GetEditShouldBeMappedCorrectly()
             => MyRouting
            .Configuration()
            .ShouldMap(request => request
                  .WithMethod(HttpMethod.Get)
                  .WithPath("/Administration/Clinics/Edit/1"))
            .To<ClinicsController>(c => c
               .Edit(1));

        [Fact]
        public void PostEditShouldBeMappedCorrectly()
             => MyRouting
            .Configuration()
            .ShouldMap(request => request
                  .WithMethod(HttpMethod.Post)
                  .WithAntiForgeryToken()
                  .WithPath("/Administration/Clinics/Edit/1"))
            .To<ClinicsController>(c => c
                 .Edit(1, new ClinicInputModel()));

        [Fact]
        public void DeleteShouldBeMappedCorrectly()
              => MyRouting
            .Configuration()
              .ShouldMap("/Administration/Clinics/Delete/1")
            .To<ClinicsController>(c => c
                .Delete(1));
    }
}
