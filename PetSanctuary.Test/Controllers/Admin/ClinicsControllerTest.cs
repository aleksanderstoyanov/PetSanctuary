namespace PetSanctuary.Test.Controllers.Admin
{
    using PetSanctuary.Web.Areas.Administration.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using PetSanctuary.Web.ViewModels.Administration.Clinics;
    using PetSanctuary.Data.Models;
    using System.Linq;
    using PetSanctuary.Test.Data;

    public class ClinicsControllerTest
    {

        [Fact]
        public void CreateShouldReturnView()
            => MyController<ClinicsController>
            .Instance()
            .WithUser(TestUser.Username, "Administration")
            .Calling(c => c.Create())
            .ShouldHave()
              .NoActionAttributes()
            .AndAlso()
            .ShouldReturn()
            .View();

        [Fact]
        public void PostCreateShouldReturnRedirectToProperActionWithValidModelState()
            => MyController<ClinicsController>
            .Instance()
             .WithUser(TestUser.Username, "Administration")
            .Calling(c => c.Create(new ClinicInputModel
            {
                Name = "TestName",
                Address = "TestAddress",
                City = "TestCity",
                Image = "http://mandoubapp.com/public/uploads/doctors/r8VRdtgphyjjSm32Azly8uTxBa5cHJ7EwdAlSZZi.jpg"
            }))
            .ShouldHave()
              .ActionAttributes(attributes =>
                 attributes.RestrictingForHttpMethod(HttpMethod.Post))
              .ValidModelState()
            .AndAlso()
            .ShouldHave()
            .Data(data => data
                .WithSet<Clinic>(data => data
                   .Any(model => model.Name == "TestName")))
            .AndAlso()
            .ShouldReturn()
            .Redirect("/Clinics/Index");

        [Fact]
        public void EditShouldReturnView()
            => MyController<ClinicsController>
            .Instance()
             .WithUser(TestUser.Username, "Administration")
             .WithData(ClinicsTestData.GetClinics(1, "Sofia"))
            .Calling(c => c.Edit(1))
            .ShouldHave()
             .NoActionAttributes()
            .AndAlso()
            .ShouldReturn()
            .View(result => result
               .WithModelOfType<ClinicInputModel>());

        [Fact]
        public void PostEditShouldRedirectToProperActionWithValidModelState()
            => MyController<ClinicsController>
            .Instance()
              .WithUser(TestUser.Username, "Administration")
              .WithData(ClinicsTestData.GetClinics(1, "Sofia"))
            .Calling(c => c.Edit(1, new ClinicInputModel
            {
                Name = "EditedName",
                City = "Sofia",
                Address = "TestAddress1",
                Image = "http://mandoubapp.com/public/uploads/doctors/r8VRdtgphyjjSm32Azly8uTxBa5cHJ7EwdAlSZZi.jpg"
            }))
            .ShouldHave()
              .ActionAttributes(attributes => attributes
                 .RestrictingForHttpMethod(HttpMethod.Post))
            .ValidModelState()
            .AndAlso()
            .ShouldHave()
            .Data(data => data
                .WithSet<Clinic>(data => data
                  .Any(model => model.Name == "EditedName")))
            .AndAlso()
            .ShouldReturn()
            .Redirect("/Clinics/Index");

        [Fact]
        public void DeleteShouldRedirectToProperAction()
            => MyController<ClinicsController>
            .Instance()
               .WithUser(TestUser.Username, "Administration")
               .WithData(ClinicsTestData.GetClinics(1, "Sofia"))
            .Calling(c => c.Delete(1))
            .ShouldHave()
            .ActionAttributes()
            .AndAlso()
            .ShouldHave()
            .Data(data => data
              .WithSet<Clinic>(data => !data
                .Any()))
            .AndAlso()
            .ShouldReturn()
            .Redirect("/Clinics/Index");

    }
}
