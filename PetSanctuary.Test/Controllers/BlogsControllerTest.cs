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
            .ActionAttributes(attributes=>attributes
                    .RestrictingForAuthorizedRequests()
                    .RestrictingForHttpMethod(HttpMethod.Post))
            .AndAlso()
            .ShouldHave()
            .ValidModelState()
              .Data(data => data.WithSet<Blog>(blogs => blogs.Any(blog => blog.Title == "TestTitle")))
            .AndAlso()
            .ShouldReturn()
            .Redirect("/Blogs");
            
           


    }
}
