namespace PetSanctuary.Test.Routing
{
    using PetSanctuary.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using PetSanctuary.Web.ViewModels.User;

    public class MyProfileControllerTest
    {
        [Fact]
        public void IndexRouteShouldBeMappedCorrectly()
            => MyRouting
              .Configuration()
            .ShouldMap("/MyProfile")
            .To<MyProfileController>(c => c
               .Index());

        [Fact]
        public void PostsRouteShouldBeMappedCorrectly()
            => MyRouting
            .Configuration()
             .ShouldMap("/MyProfile/Posts")
            .To<MyProfileController>(c => c
                .Posts(new PetPostQueryModel()));


        [Fact]
        public void BlogsShouldBeMappedCorrectly()
            => MyRouting
            .Configuration()
             .ShouldMap("/MyProfile/Blogs")
            .To<MyProfileController>(c => c
               .Blogs(new BlogPostQueryModel()));
    }
}
