namespace PetSanctuary.Test.Routing
{
    using PetSanctuary.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using PetSanctuary.Web.ViewModels.Catalog;

    public class CatalogControllerTest
    {
        [Fact]
        public void IndexShouldBeMappedCorrectly()
             => MyRouting
             .Configuration()
               .ShouldMap("/Catalog")
             .To<CatalogController>(c =>
               c.Index(new CatalogQueryModel()));


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
        public void GetEditShouldBeMappedCorrectly()
             => MyRouting
            .Configuration()
            .ShouldMap(request => request
                   .WithPath("/Catalog/Edit/TestId")
                   .WithMethod(HttpMethod.Get))
            .To<CatalogController>(c => c
                 .Edit("TestId"));

        [Fact]
        public void PostEditShouldBeMappedCorrectly()
             => MyRouting
            .Configuration()
            .ShouldMap(request => request
                  .WithPath("/Catalog/Edit/TestId")
                  .WithMethod(HttpMethod.Post)
                  .WithAntiForgeryToken())
            .To<CatalogController>(c => c
                 .Edit("TestId"));

        [Fact]
        public void DeleteShouldBeMappedCorrectly()
               => MyRouting
            .Configuration()
            .ShouldMap("/Catalog/Delete/TestId")
            .To<CatalogController>(c => c
                 .Delete("TestId"));

        [Fact]
        public void DetailsShouldBeMappedCorrectly()
            => MyRouting
            .Configuration()
            .ShouldMap("/Catalog/Details/Test")
            .To<CatalogController>(c => c.Details("Test"));

    }
}
