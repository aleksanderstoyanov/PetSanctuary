namespace PetSanctuary.Test.Routing
{
    using MyTested.AspNetCore.Mvc;
    using PetSanctuary.Web.Controllers;
    using Xunit;
    public class HomeControllerTest
    {
        [Fact]
        public void IndexRouteShouldBeMappedCorrectly()
           => MyRouting
            .Configuration()
            .ShouldMap("/")
            .To<HomeController>(c => c.Index());

        [Fact]
        public void ErrorRouteShouldBeMappedCorrectly()
           => MyRouting
            .Configuration()
            .ShouldMap("/Home/Error")
            .To<HomeController>(c => c.Error());
    }
}
