namespace PetSanctuary.Test.Pipeline.Administration
{
    using PetSanctuary.Web.Areas.Administration.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using PetSanctuary.Common;
    using PetSanctuary.Web.ViewModels.Administration.Vets;
    using PetSanctuary.Data.Models;
    using System.Linq;
    using PetSanctuary.Test.Data;

    public class VetsPipelineTest
    {
        [Fact]
        public void GetCreateShouldReturnDefaultView()
            => MyPipeline
            .Configuration()
            .ShouldMap(request => request
              .WithLocation("/Administration/Vets/Create")
              .WithUser(new[] { GlobalConstants.AdministratorRoleName }))
            .To<VetsController>(c => c.Create())
            .Which()
            .ShouldReturn()
            .View();

        [Theory]
        [InlineData("TestFirstName", "TestSurname", "Test1", "Veterinarian", "Test Description...")]
        public void PostCreateShouldReturnDefaultRedirectToActionWithValidModelState(string firstName, string surname, string clinic, string qualification, string description)
            => MyPipeline
            .Configuration()
            .ShouldMap(request => request
                .WithMethod(HttpMethod.Post)
                .WithLocation("/Administration/Vets/Create/TestId")
                .WithFormFields(new
                {
                    FirstName = firstName,
                    Surname = surname,
                    Clinic = clinic,
                    Qualification = qualification,
                    Description = description
                })
                .WithUser(new[] { GlobalConstants.AdministratorRoleName })
                .WithAntiForgeryToken())
               .To<VetsController>(c => c.Create(new VetInputModel
               {
                   FirstName = firstName,
                   Surname = surname,
                   Clinic = clinic,
                   Qualification = qualification,
                   Description = description
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
              .WithSet<Vet>(data => data.Any()))
            .AndAlso()
            .ShouldReturn()
            .Redirect("/Clinics/Index");


        [Fact]
        public void GetEditShouldReturnDefaultView()
            => MyPipeline
            .Configuration()
            .ShouldMap(request => request
               .WithLocation("/Administration/Vets/Edit/TestId1")
               .WithUser(new[] { GlobalConstants.AdministratorRoleName }))
            .To<VetsController>(c => c.Edit("TestId1"))
            .Which(controller => controller
               .WithData(VetsTestData.GetVets(1)))
            .ShouldReturn()
            .View(result => result
               .WithModelOfType<VetInputModel>());

        [Theory]
        [InlineData("EditedFirstName", "TestSurname", "Test", "Veterinarian", "Test Description...")]
        public void PostEditShouldReturnDefaultRedirectToActionWithValidModelState(string firstName, string surname, string clinic, string qualification, string description)
            => MyPipeline
            .Configuration()
            .ShouldMap(request => request
                .WithMethod(HttpMethod.Post)
                .WithQuery("clinicId","1")
                .WithLocation("/Administration/Vets/Edit/TestId1")
                .WithFormFields(new
                {
                    FirstName = firstName,
                    Surname = surname,
                    Clinic = clinic,
                    Qualification = qualification,
                    Description = description
                })
                .WithUser(new[] { GlobalConstants.AdministratorRoleName })
                .WithAntiForgeryToken())
            .To<VetsController>(c => c.Edit(1, "TestId1", new VetInputModel
            {
                FirstName = firstName,
                Surname = surname,
                Clinic = clinic,
                Qualification = qualification,
                Description = description
            }))
            .Which(controller => controller
               .WithData(VetsTestData.GetVets(1)))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post))
            .AndAlso()
            .ShouldHave()
            .ValidModelState()
            .Data(data => data
              .WithSet<Vet>(data => data.Any(model => model.FirstName == firstName)))
            .AndAlso()
            .ShouldReturn()
            .Redirect("/Vets/Index/1");


        [Fact]
        public void GetDeleteShouldReturnDefaultRedirectToAction()
            => MyPipeline
            .Configuration()
            .ShouldMap(request => request
                .WithLocation("/Administration/Vets/Delete/TestId1")
                .WithQuery("clinicId","1")
                .WithUser(new[] { GlobalConstants.AdministratorRoleName }))
            .To<VetsController>(c => c.Delete(1, "TestId1"))
            .Which(controller => controller
               .WithData(VetsTestData.GetVets(1)))
            .ShouldHave()
            .Data(data => data
               .WithSet<Vet>(data => !data.Any()))
            .AndAlso()
            .ShouldReturn()
            .Redirect("/Vets/Index/1");



    }
}
