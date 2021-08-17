namespace PetSanctuary.Test.Pipeline.Administration
{
    using PetSanctuary.Web.Areas.Administration.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using PetSanctuary.Common;
    using PetSanctuary.Web.ViewModels.Administration.Clinics;
    using PetSanctuary.Data.Models;
    using System.Linq;
    using PetSanctuary.Test.Data;

    public class ClinicsPipelineTest
    {
        [Fact]
        public void GetCreateShouldReturnDefaultView()
            => MyPipeline
            .Configuration()
            .ShouldMap(request => request
                .WithLocation("/Administration/Clinics/Create")
                .WithUser(new[] { GlobalConstants.AdministratorRoleName }))
            .To<ClinicsController>(c => c
                .Create())
            .Which()
            .ShouldReturn()
            .View();

        [Theory]
        [InlineData("TestName", "TestAddress", "TestCity", "http://mandoubapp.com/public/uploads/doctors/r8VRdtgphyjjSm32Azly8uTxBa5cHJ7EwdAlSZZi.jpg")]
        public void PostCreateShouldReturnDefaultRedirectToActionAndShouldBeWithValidModelState(string name, string address, string city, string image)
            => MyPipeline
            .Configuration()
            .ShouldMap(request => request
                .WithMethod(HttpMethod.Post)
                .WithFormFields(new
                {
                    Name = name,
                    Address = address,
                    City = city,
                    Image = image
                })
                .WithUser(new[] { GlobalConstants.AdministratorRoleName })
                .WithAntiForgeryToken()
                .WithLocation("/Administration/Clinics/Create"))
            .To<ClinicsController>(c => c.Create(new ClinicInputModel
            {
                Name = name,
                Address = address,
                City = city,
                Image = image
            }))
            .Which()
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post))
            .AndAlso()
            .ShouldHave()
            .ValidModelState()
            .Data(data => data
                .WithSet<Clinic>(data => data.Any()))
            .AndAlso()
            .ShouldReturn()
            .Redirect("/Clinics/Index");

        [Fact]
        public void GetEditShouldReturnDefaultView()
            => MyPipeline
            .Configuration()
            .ShouldMap(request => request
                 .WithLocation("/Administration/Clinics/Edit/1")
                 .WithUser(new[] { GlobalConstants.AdministratorRoleName }))
            .To<ClinicsController>(c => c.Edit(1))
            .Which(controller => controller
               .WithData(ClinicsTestData.GetClinics(1, "Sofia")))
            .ShouldReturn()
            .View(result => result
               .WithModelOfType<ClinicInputModel>());

        [Theory]
        [InlineData("EditedName", "TestAddress", "TestCity", "http://mandoubapp.com/public/uploads/doctors/r8VRdtgphyjjSm32Azly8uTxBa5cHJ7EwdAlSZZi.jpg")]
        public void PostEditShouldReturnDefaultRedirectToActionAndShouldBeWithValidModelState(string name, string address, string city, string image)
            => MyPipeline
            .Configuration()
            .ShouldMap(request => request
                 .WithMethod(HttpMethod.Post)
                 .WithLocation("/Administration/Clinics/Edit/1")
                 .WithFormFields(new
                 {
                     Name = name,
                     Address = address,
                     City = city,
                     Image = image
                 })
                 .WithUser(new[] { GlobalConstants.AdministratorRoleName })
                 .WithAntiForgeryToken())
            .To<ClinicsController>(c => c.Edit(1, new ClinicInputModel
            {
                Name = name,
                Address = address,
                City = city,
                Image = image
            }))
            .Which(controller => controller
                .WithData(ClinicsTestData.GetClinics(1, "Sofia")))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post))
            .AndAlso()
            .ShouldHave()
            .ValidModelState()
            .Data(data => data
                .WithSet<Clinic>(data => data
                   .Any(data => data.Name == name)))
            .AndAlso()
            .ShouldReturn()
            .Redirect("/Clinics/Index");

        [Fact]
        public void GetDeleteShouldReturnDefaultRedirectToAction()
            => MyPipeline
            .Configuration()
            .ShouldMap(request => request
               .WithLocation("Administration/Clinics/Delete/1")
               .WithUser(new[] { GlobalConstants.AdministratorRoleName }))
            .To<ClinicsController>(c => c.Delete(1))
            .Which(controller => controller
               .WithData(ClinicsTestData.GetClinics(1, "Sofia")))
            .ShouldHave()
            .Data(data => data
              .WithSet<Clinic>(data => !data.Any()))
            .AndAlso()
            .ShouldReturn()
            .Redirect("/Clinics/Index");

    }
}
