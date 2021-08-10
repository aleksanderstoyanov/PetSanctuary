namespace PetSanctuary.Test.Controllers
{
    using PetSanctuary.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using PetSanctuary.Web.ViewModels.User;
    using PetSanctuary.Data.Models;
    using PetSanctuary.Test.Data;
    using PetSanctuary.Data.Models.Enums;

    public class MyProfileControllerTest
    {
        [Fact]
        public void IndexShouldReturnProperViewWithModel()
            => MyController<MyProfileController>
            .Instance()
            .WithUser()
              .WithData(new ApplicationUser
              {
                  Id = TestUser.Identifier,
                  UserName = TestUser.Username
              })
            .Calling(c => c.Index())
            .ShouldHave()
               .ActionAttributes(attributes => attributes
                 .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View(result => result
                .WithModelOfType<ProfileViewModel>());

        [Fact]
        public void PostsShouldBeForAuthorizedUsersAndShouldReturnViewWithValidModel()
            => MyController<MyProfileController>
            .Instance()
              .WithUser()
              .WithData(PetTestData.GetPets(2, PetType.Dog))
            .Calling(c => c.Posts(new PetPostQueryModel()))
             .ShouldHave()
             .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View(result => result
                .WithModelOfType<PetPostQueryModel>(model => model
                     .Pets.Count == 2));

        [Fact]
        public void BlogsShouldBeForAuthorizedUsersAndShouldReturnViewWithValidModel()
               => MyController<MyProfileController>
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
            .Calling(c => c.Blogs(new BlogPostQueryModel()))
              .ShouldHave()
              .ActionAttributes(attributes => attributes
                   .RestrictingForAuthorizedRequests())
            .AndAlso()
             .ShouldReturn()
             .View(result => result
               .WithModelOfType<BlogPostQueryModel>(model => model
                   .Blogs.Count == 1));

    }
}
