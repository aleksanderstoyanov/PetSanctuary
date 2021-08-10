namespace PetSanctuary.Test.Pipeline
{
    using PetSanctuary.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    public class HomePipelineTest
    {
        [Fact]
        public void GetIndexShouldReturnDefaultView()
            => MyMvc
              .Pipeline()
            .ShouldMap("/")
              .To<HomeController>(c => c.Index())
              .Which()
            .ShouldReturn()
            .View();

        [Fact]
        public void GetPrivacyShouldReturnDefaultView()
            => MyMvc
            .Pipeline()
           .ShouldMap("/Home/Privacy")
            .To<HomeController>(c => c.Privacy())
              .Which()
            .ShouldReturn()
            .View();

        [Fact]
        public void GetErrorShouldReturnDefaultView()
            => MyMvc
            .Pipeline()
           .ShouldMap("/Home/Error")
             .To<HomeController>(c => c.Error())
            .Which()
             .ShouldReturn()
            .View();
    }
}
