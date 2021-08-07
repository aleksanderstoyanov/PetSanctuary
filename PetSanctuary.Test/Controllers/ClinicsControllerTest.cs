namespace PetSanctuary.Test.Controllers
{
    using PetSanctuary.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using PetSanctuary.Web.ViewModels.Clinics;
    using PetSanctuary.Services.Data.Clinics;
    using System.Collections.Generic;
    using PetSanctuary.Test.Data;

    public class ClinicsControllerTest
    {

        [Theory]
        [InlineData(3, "Sofia")]
        [InlineData(2, "Varna")]
        [InlineData(1, "Burgas")]
        public void IndexShouldBeMappedProperlyWithCorrectViewModel(int count, string city)
             => MyController<ClinicsController>
            .Instance()
            .WithData(ClinicsTestData.GetClinics(count, city))
            .Calling(c => c.Index(new ClinicsQueryModel
            {
                City = city

            }))
            .ShouldHave()
            .MemoryCache(m => m
            .ContainingEntryOfType<List<ClinicServiceModel>>())
            .AndAlso()
            .ShouldReturn()
            .View(result => result
                .WithModelOfType<ClinicsQueryModel>()
                 .Passing(model => model
                 .Clinics.Count == count));



    }
}
