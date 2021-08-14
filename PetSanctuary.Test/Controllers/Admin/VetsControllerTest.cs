namespace PetSanctuary.Test.Controllers.Admin
{
    using PetSanctuary.Web.Areas.Administration.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using PetSanctuary.Web.ViewModels.Administration.Vets;
    using PetSanctuary.Test.Data;
    using PetSanctuary.Data.Models;
    using System.Linq;

    public class VetsControllerTest
    {
        [Fact]
        public void CreateShouldReturnProperView()
            => MyController<VetsController>
            .Instance()
              .WithUser(TestUser.Username, "Administration")
            .Calling(c => c.Create())
            .ShouldHave()
            .NoActionAttributes()
            .AndAlso()
            .ShouldReturn()
            .View();

        [Fact]
        public void PostCreateShouldReturnProperRedirectToActionWithValidModelState()
            => MyController<VetsController>
            .Instance()
              .WithUser(TestUser.Username, "Administration")
              .WithData(ClinicsTestData.GetClinics(1, "Sofia"))
            .Calling(c => c.Create(new VetInputModel
            {
                FirstName = "TestFirstName",
                Surname = "TestSurname",
                Clinic = "Test1",
                Qualification = "Veterinary",
                Description = "Test description..."
            }))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
               .RestrictingForHttpMethod(HttpMethod.Post))
            .AndAlso()
            .ShouldHave()
            .ValidModelState()
            .Data(data => data
                .WithSet<Vet>(data => data.Any(vet => vet
                      .FirstName == "TestFirstName")))
            .AndAlso()
            .ShouldReturn()
            .Redirect("/Clinics/Index");

        [Fact]
        public void EditShouldReturnProperViewWithModel()
            => MyController<VetsController>
            .Instance()
              .WithUser(TestUser.Username, "Administration")
              .WithData(VetsTestData.GetVets(1))
            .Calling(c => c.Edit("TestId1"))
            .ShouldReturn()
            .View(result => result
                .WithModelOfType<VetInputModel>());

        [Fact]
        public void PostEditShouldReturnProperRedirectToActionWithValidModelState()
            => MyController<VetsController>
            .Instance()
              .WithUser(TestUser.Username, "Administration")
              .WithData(VetsTestData.GetVets(1))
            .Calling(c => c.Edit(1, "TestId1", new VetInputModel
            {
                FirstName = "EditedFirstName",
                Surname = "TestSurname",
                Clinic = "Test",
                Qualification = "Veterinary",
                Description = "Test description..."
            }))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
               .RestrictingForHttpMethod(HttpMethod.Post))
            .AndAlso()
            .ShouldHave()
            .Data(data => data
                 .WithSet<Vet>(data => data
                 .Any(vet => vet.FirstName == "EditedFirstName")))
            .AndAlso()
            .ShouldReturn()
            .Redirect("/Vets/Index/1");

        [Fact]
        public void DeleteShouldReturnProperRedirectToAction()
            =>MyController<VetsController>
              .Instance()
                .WithUser(TestUser.Username, "Administration")
                .WithData(VetsTestData.GetVets(1))
            .Calling(c=>c.Delete(1,"TestId1"))
            .ShouldHave()
              .ActionAttributes()
            .AndAlso()
            .ShouldHave()
            .Data(data=>data
                .WithSet<Vet>(data=>!data.Any()))
            .AndAlso()
            .ShouldReturn()
             .Redirect("/Vets/Index/1");





    }
}
