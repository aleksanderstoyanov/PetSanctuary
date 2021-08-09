namespace PetSanctuary.Test.Controllers
{
    using PetSanctuary.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using PetSanctuary.Services.Data.Blogs;
    using System.Collections.Generic;
    using PetSanctuary.Data.Models;
    using Microsoft.AspNetCore.Http;
    using System.IO;
    using PetSanctuary.Web.ViewModels.Blogs;
    using System.Linq;

    public class BlogsControllerTest
    {
        [Fact]
        public void IndexShouldReturnProperView()
            => MyController<BlogsController>
            .Instance()
            .Calling(c => c.Index())
            .ShouldReturn()
            .View(view => view
                .WithModelOfType<List<BlogServiceModel>>());


        [Fact]
        public void GetCreateShouldReturnProperViewAndShouldBeWithAuthorizedUsers()
            => MyController<BlogsController>
            .Instance()
            .Calling(c => c.Create())
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                 .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View();

        [Fact]
        public void PostCreateShouldReturnProperViewAndShouldBeWithAuthorizedUsersAndWithValidModel()
           => MyController<BlogsController>
            .Instance(c => c
               .WithUser())
            .Calling(c => c.Create(new BlogFormCreateViewModel
            {
                Title = "TestTitle",
                Description = "Test description blog",
                Image = new FormFile(File.OpenRead(@"C:\Users\black\OneDrive\Desktop\PetSanctuary\Web\PetSanctuary.Web\wwwroot\img\Test.png"), 0, "Test.png".Length, null, "Test.png"),
            }))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests()
                    .RestrictingForHttpMethod(HttpMethod.Post))
            .AndAlso()
            .ShouldHave()
            .ValidModelState()
              .Data(data => data.WithSet<Blog>(blogs => blogs.Any(blog => blog.Title == "TestTitle")))
            .AndAlso()
            .ShouldReturn()
            .Redirect("/Blogs");


        [Fact]
        public void GetEditShouldBeForAuthorizedUsersAndShoudReturnView()
             => MyController<BlogsController>
             .Instance()
               .WithUser()
               .WithData(new Blog
               {
                   Id = "TestBlogId1",
                   Title = "TestBlogTitle",
                   Description = "Test description blog",
                   Image = "TestImage",
                   Author = new ApplicationUser
                   {
                       Id = TestUser.Identifier,
                       UserName = TestUser.Username
                   }


               })
            .Calling(c => c.Edit("TestBlogId1"))
            .ShouldHave()
              .ActionAttributes(attributes => attributes
                   .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View(result => result
                   .WithModelOfType<BlogEditFormModel>());

        [Fact]
        public void PostEditShouldBeForAuthorizedUsersAndShouldReturnRedirectToActionWithProperModel()
            => MyController<BlogsController>
            .Instance()
            .WithUser()
            .WithData(new Blog
            {
                Id = "TestBlogId1",
                Title = "TestBlogTitle",
                Description = "Test description blog",
                Image = "TestImage",
                Author = new ApplicationUser
                {
                    Id = TestUser.Identifier,
                    UserName = TestUser.Username
                }


            })
            .Calling(c => c.Edit("TestBlogId1", new BlogEditFormModel
            {
                Title = "EditedBlogTitle",
                Description = "Test description blog"
            }))
              .ShouldHave()
            .ActionAttributes(attributes => attributes
                  .RestrictingForHttpMethod(HttpMethod.Post)
                  .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldHave()
              .Data(data => data
                    .WithSet<Blog>(data => data
                         .Any(blog => blog.Title == "EditedBlogTitle")))
            .AndAlso()
             .ShouldReturn()
            .RedirectToAction("Blogs", "MyProfile");

        [Fact]
        public void DeleteShouldBeForAuthorizedUsersAndShouldRedirectToProperControllerWithAction()
            => MyController<BlogsController>
               .Instance()
            .WithUser()
            .WithData(new Blog
            {
                Id = "TestBlogId1",
                Title = "TestBlogTitle",
                Description = "Test description blog",
                Image = "TestImage",
                Author = new ApplicationUser
                {
                    Id = TestUser.Identifier,
                    UserName = TestUser.Username
                }


            })
            .Calling(c => c.Delete("TestBlogId1"))
              .ShouldHave()
              .ActionAttributes(attributes => attributes
                   .RestrictingForAuthorizedRequests())
            .AndAlso()
             .ShouldHave()
               .Data(data => data
                  .WithSet<Blog>(data => data.Count() == 0))
            .AndAlso()
            .ShouldReturn()
            .RedirectToAction("Blogs", "MyProfile");


    }
}
