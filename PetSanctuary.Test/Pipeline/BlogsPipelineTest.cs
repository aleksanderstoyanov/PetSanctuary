namespace PetSanctuary.Test.Pipeline
{
    using PetSanctuary.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using PetSanctuary.Data.Models;
    using PetSanctuary.Test.Data;
    using PetSanctuary.Web.ViewModels.Blogs;
    using System.Linq;

    public class BlogsPipelineTest
    {
        [Fact]
        public void GetIndexShouldReturnDefaultViewWithValidModel()
            => MyPipeline
            .Configuration()
            .ShouldMap("/Blogs")
              .To<BlogsController>(c => c.Index())
            .Which()
              .ShouldReturn()
            .View();

        [Fact]
        public void GetRecentShouldReturnDefaultViewWithValidModel()
            => MyPipeline
            .Configuration()
            .ShouldMap("/Blogs/Recent")
              .To<BlogsController>(c => c.Recent())
            .Which()
              .ShouldReturn()
            .View();

        [Fact]
        public void GetAllShouldReturnDefaultViewWithValidModel()
            => MyPipeline
            .Configuration()
            .ShouldMap("/Blogs/All")
              .To<BlogsController>(c => c.All(new BlogQueryModel()))
            .Which(controller => controller
              .WithData(BlogTestData.GetBlogs(1)))
            .ShouldReturn()
            .View(result => result
              .WithModelOfType<BlogQueryModel>()
              .Passing(model => model
                 .Blogs.Count() == 1));

        [Fact]
        public void GetCreateShouldReturnDefaultViewWithValidModel()
            => MyPipeline
            .Configuration()
            .ShouldMap(request => request
                .WithPath("/Blogs/Create")
                .WithUser())
            .To<BlogsController>(c => c
                .Create())
            .Which()
               .ShouldReturn()
            .View();

        [Fact]
        public void GetEditShouldReturnDefaultViewWithValidModelAndShoulBeForAuthorizedUsers()
            => MyPipeline
            .Configuration()
            .ShouldMap(request => request
                 .WithMethod(HttpMethod.Post)
                 .WithPath("/Blogs/Edit/TestBlogId1")
                 .WithUser()
                 .WithAntiForgeryToken())
            .To<BlogsController>(c => c
               .Edit("TestBlogId1"))
            .Which(controller => controller
                .WithData(BlogTestData.GetBlogs(1)))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
              .ShouldReturn()
            .View();

        [Theory]
        [InlineData("EditedBlogTitle", "Test description blog")]
        [InlineData("EditedBlogTitle2", "Test description blog2")]
        [InlineData("EditedBlogTitle3", "Test description blog3")]
        public void PostEditShouldReturnDefaultRedirectWithValidModelAndShouldBeForAuthorizedUsers(string title, string description)
            => MyPipeline
            .Configuration()
            .ShouldMap(request => request
                    .WithMethod(HttpMethod.Post)
                    .WithPath("/Blogs/Edit/TestBlogId1")
                    .WithFormFields(new
                    {
                        Title = title,
                        Description = description
                    })
                    .WithUser()
                    .WithAntiForgeryToken())
            .To<BlogsController>(c => c
               .Edit("TestBlogId1", new BlogEditFormModel
               {
                   Title = title,
                   Description = description
               }))
            .Which(controller => controller
                 .WithData(BlogTestData.GetBlogs(1)))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldHave()
             .TempData(tempData => tempData
             .ContainingEntryWithKey("message"))
             .ValidModelState()
             .Data(data => data
               .WithSet<Blog>(model => model
                  .Any(blog => blog
                      .Title == title)))
            .AndAlso()
            .ShouldReturn()
            .RedirectToAction("Blogs", "MyProfile");

        [Fact]
        public void GetDeleteShouldRedirectToDefaultAction()
            => MyPipeline
            .Configuration()
            .ShouldMap(request => request
                .WithPath("/Blogs/Delete/TestBlogId1")
                .WithUser()
                .WithAntiForgeryToken())
            .To<BlogsController>(c => c
                 .Delete("TestBlogId1"))
            .Which(controller => controller
                .WithData(BlogTestData.GetBlogs(1)))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
               .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldHave()
              .TempData(tempData => tempData
              .ContainingEntryWithKey("message"))
              .Data(data => data.WithSet<Blog>(data => !data.Any()))
            .AndAlso()
            .ShouldReturn()
            .RedirectToAction("Blogs", "MyProfile");
    }
}
