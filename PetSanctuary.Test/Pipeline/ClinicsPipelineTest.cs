namespace PetSanctuary.Test.Pipeline
{
    using PetSanctuary.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using PetSanctuary.Web.ViewModels.Clinics;
    using PetSanctuary.Test.Data;
    using System.Linq;

    public class ClinicsPipelineTest
    {
        [Theory]
        [InlineData(3, "Sofia")]
        [InlineData(2,"Plovdiv")]
        public void GetIndexShouldReturnDefaultViewWithValidModel(int count, string city)
            => MyMvc
            .Pipeline()
             .ShouldMap("/Clinics")
            .To<ClinicsController>(c => c
               .Index(new ClinicsQueryModel()))
            .Which(controller => controller
                  .WithData(ClinicsTestData.GetClinics(count, city)))
            .ShouldReturn()
              .View(result => result
                 .WithModelOfType<ClinicsQueryModel>()
                  .Passing(model => model
                     .Clinics.Count() == count));
    }
}
