namespace PetSanctuary.Test.Routing
{
    using PetSanctuary.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using PetSanctuary.Web.ViewModels.Catalog;

    public class CatalogControllerTest
    {
        [Fact]
        public void DogsRouteShouldBeMappedCorrectly()
           => MyRouting
            .Configuration()
            .ShouldMap("/Catalog/Dogs")
            .To<CatalogController>(c => c.Dogs());

        [Fact]
        public void CatsRouteShouldBeMappedCorrectly()
            => MyRouting
            .Configuration()
            .ShouldMap("/Catalog/Cats")
            .To<CatalogController>(c => c.Cats());

        [Fact]
        public void OthersRouteShouldBeMappedCorrectly()
            => MyRouting
            .Configuration()
            .ShouldMap("/Catalog/Other")
            .To<CatalogController>(c => c.Other());

        [Fact]
        public void GetCreateShouldBeMappedCorrectly()
            => MyRouting
            .Configuration()
            .ShouldMap(request => request
                 .WithPath("/Catalog/Create")
                 .WithMethod(HttpMethod.Get))
            .To<CatalogController>(c => c.Create());

        [Fact]
        public void PostCreateShouldBeMappedCorrectly()
            => MyRouting
            .Configuration()
            .ShouldMap(request => request
                  .WithPath("/Catalog/Create")
                  .WithMethod(HttpMethod.Post))
            .To<CatalogController>(c => c
                  .Create(new CatalogFormCreateViewModel()));

        [Fact]
        public void DetailsShouldBeMappedCorrectly()
            => MyRouting
            .Configuration()
            .ShouldMap("/Catalog/Details/Test")
            .To<CatalogController>(c => c.Details("Test"));
            
    }
}
