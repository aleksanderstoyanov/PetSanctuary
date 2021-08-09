namespace PetSanctuary.Test.Routing
{
    using PetSanctuary.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using PetSanctuary.Web.ViewModels.Blogs;

    public class BlogsControllerTest
    {
        [Fact]
        public void IndexShouldBeMappedCorrectly()
            => MyRouting
            .Configuration()
            .ShouldMap("/Blogs")
            .To<BlogsController>(c => c.Index());

        [Fact]
        public void GetCreateShouldBeMappedCorrectly()
             => MyRouting
            .Configuration()
            .ShouldMap("/Blogs/Create")
            .To<BlogsController>(c => c.Create());

        [Fact]
        public void PostCreateShouldBeMappedCorrectlyAndWithValidModel()
             => MyRouting
            .Configuration()
            .ShouldMap(request => request
                   .WithPath("/Blogs/Create")
                   .WithMethod(HttpMethod.Post))
            .To<BlogsController>(c => c
                 .Create(new BlogFormCreateViewModel()));

        [Fact]
        public void GetEditShouldBeMappedCorrectly()
             => MyRouting
            .Configuration()
            .ShouldMap(request => request
                  .WithPath("/Blogs/Edit/TestId"))
            .To<BlogsController>(c => c
                  .Edit("TestId"));

        [Fact]
        public void PostEditShouldBeMappedCorrectly()
             => MyRouting
            .Configuration()
            .ShouldMap(request => request
                   .WithPath("/Blogs/Edit/TestId")
                   .WithMethod(HttpMethod.Post))
            .To<BlogsController>(c => c
                   .Edit("TestId",new BlogEditFormModel()));

        [Fact]
        public void DeleteShouldBeMappedCorrectly()
            => MyRouting
             .Configuration()
            .ShouldMap("/Blogs/Delete/TestId")
            .To<BlogsController>(c => c
                   .Delete("TestId"));
    }
}
