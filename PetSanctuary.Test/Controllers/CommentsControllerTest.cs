namespace PetSanctuary.Test.Controllers
{
    using PetSanctuary.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using PetSanctuary.Test.Data;
    using PetSanctuary.Web.ViewModels.Comments;
    using PetSanctuary.Data.Models;
    using System.Linq;

    public class CommentsControllerTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void BlogShouldReturnProperView(int count)
            => MyController<CommentsController>
            .Instance()
            .WithData(BlogCommentsTestData
                  .GetComments(count))
            .Calling(c => c
                .Blog("TestBlogId", new CommentQueryModel()))
            .ShouldReturn()
            .View(result => result
                .WithModelOfType<CommentQueryModel>(model => model
                        .Comments.Count == count));

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void VetShouldReturnProperView(int count)
             => MyController<CommentsController>
            .Instance()
            .WithData(VetCommentsTestData
                  .GetComments(count))
            .Calling(c => c
                .Vet("TestVetId", new CommentQueryModel()))
            .ShouldReturn()
            .View(result => result
                .WithModelOfType<CommentQueryModel>(model => model
                        .Comments.Count == count));

        [Fact]
        public void GetCreateShouldReturnProperView()
            => MyController<CommentsController>
            .Instance()
            .Calling(c => c.Create())
            .ShouldReturn()
            .View();

        [Theory]
        [InlineData("Test data 123 123")]
        [InlineData("So happy for Tom")]

        public void PostCreateForBlogShouldBeForAuthorizedUsersAndShouldRedirectToProperAction(string content)
            => MyController<CommentsController>
            .Instance()
               .WithData(BlogCommentsTestData.GetComments(2))
               .WithUser()
            .Calling(c => c
               .Create("TestBlogId", "Blog", new CommentInputModel
               {
                   Content = content
               }))
            .ShouldHave()
              .ActionAttributes(attributes => attributes
                   .RestrictingForHttpMethod(HttpMethod.Post)
                   .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldHave()
              .TempData(tempData => tempData
              .ContainingEntryWithKey("message"))
              .Data(data => data
                   .WithSet<Comment>(data => data
                      .Any(comment => comment
                           .Content == content)))
              .AndAlso()
              .ShouldReturn()
                 .RedirectToAction("Blog", "Comments", new { id = "TestBlogId" });

        [Theory]
        [InlineData("Test vet comment")]
        [InlineData("Doctor is really good")]
        public void PostCreateForVetShouldBeForAuthorizedUsersAndShouldRedirectToProperAction(string content)
            => MyController<CommentsController>
              .Instance()
            .Calling(c => c
                .Create("TestVetId", "Vet", new CommentInputModel
                {
                    Content = content
                }))
            .ShouldHave()
               .ActionAttributes(attributes => attributes
                      .RestrictingForAuthorizedRequests()
                      .RestrictingForHttpMethod(HttpMethod.Post))
            .AndAlso()
            .ShouldHave()
               .TempData(tempData => tempData
               .ContainingEntryWithKey("message"))
               .Data(data => data
                    .WithSet<Comment>(data => data.
                       Any(comment => comment.Content == content)))
            .AndAlso()
            .ShouldReturn()
            .RedirectToAction("Vet", "Comments", new { id = "TestVetId" });

        [Fact]
        public void GetEditForBlogsShouldReturnProperView()
              => MyController<CommentsController>
                .Instance()
                  .WithData(BlogCommentsTestData.GetComments(2))
                .Calling(c => c.Edit(1))
            .ShouldReturn()
              .View();

        [Fact]
        public void GetEditForVetsShouldReturnProperView()
             => MyController<CommentsController>
               .Instance()
                 .WithData(VetCommentsTestData.GetComments(2))
               .Calling(c => c.Edit(1))
            .ShouldReturn()
              .View();

        [Theory]
        [InlineData("EditedTestId")]
        public void PostEditForBlogsShouldBeForAuthorizedUsersAndShouldReturnProperView(string content)
               => MyController<CommentsController>
                 .Instance()
            .WithData(BlogCommentsTestData.GetComments(2))
                .Calling(c => c
                   .Edit(1, "Blog", new CommentInputModel
                   {
                       Content = content
                   }))
            .ShouldHave()
              .ActionAttributes(attributes => attributes
                   .RestrictingForHttpMethod(HttpMethod.Post)
                   .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldHave()
              .TempData(tempData => tempData
              .ContainingEntryWithKey("message"))
              .Data(data => data
                  .WithSet<Comment>(data => data
                    .Any(comment => comment.Content == content)))
            .AndAlso()
            .ShouldReturn()
             .RedirectToAction("Blog", "Comments", new { id = "TestBlogId" });

        [Theory]
        [InlineData("EditedTestId")]
        public void PostEditForVetsShouldBeForAuthorizedUserAndShouldReturnProperView(string content)
             => MyController<CommentsController>
                .Instance()
                .WithData(VetCommentsTestData.GetComments(2))
                  .Calling(c => c
                     .Edit(1, "Vet", new CommentInputModel
                     {
                         Content=content
                     }))
            .ShouldHave()
               .ActionAttributes(attributes=>attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
             .AndAlso()
            .ShouldHave()
              .TempData(tempData => tempData
              .ContainingEntryWithKey("message"))
              .Data(data => data
                  .WithSet<Comment>(data => data
                    .Any(comment => comment.Content == content)))
            .AndAlso()
            .ShouldReturn()
             .RedirectToAction("Vet", "Comments", new { id = "TestVetId" });

        [Fact]
        public void DeleteForBlogsShouldRedirectToProperAction()
             =>MyController<CommentsController>
               .Instance()
                .WithData(BlogCommentsTestData.GetComments(2))
               .Calling(c=>c
                   .Delete(1,"Blog"))
            .ShouldHave()
               .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
             .AndAlso()
            .ShouldHave()
              .TempData(tempData => tempData
              .ContainingEntryWithKey("message"))
              .Data(data => data
                  .WithSet<Comment>(data => data
                    .Count()==1))
            .AndAlso()
            .ShouldReturn()
             .RedirectToAction("Blog", "Comments", new { id = "TestBlogId" });

        [Fact]
        public void DeleteForVetsShouldRedirectToProperAction()
           => MyController<CommentsController>
             .Instance()
              .WithData(VetCommentsTestData.GetComments(2))
             .Calling(c => c
                 .Delete(1, "Vet"))
          .ShouldHave()
             .ActionAttributes(attributes => attributes
                  .RestrictingForAuthorizedRequests())
           .AndAlso()
          .ShouldHave()
            .TempData(tempData => tempData
            .ContainingEntryWithKey("message"))
            .Data(data => data
                .WithSet<Comment>(data => data
                  .Count() == 1))
          .AndAlso()
          .ShouldReturn()
           .RedirectToAction("Vet", "Comments", new { id = "TestVetId" });

    }
}
