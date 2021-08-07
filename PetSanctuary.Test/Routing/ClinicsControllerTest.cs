namespace PetSanctuary.Test.Routing
{
    using PetSanctuary.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using PetSanctuary.Web.ViewModels.Clinics;

    public class ClinicsControllerTest
    {
        [Fact]
        public void IndexShouldBeMappedCorrectly()
          => MyRouting
            .Configuration()
            .ShouldMap("/Clinics")
            .To<ClinicsController>(c=>c.Index(new ClinicsQueryModel()));
    }
}
