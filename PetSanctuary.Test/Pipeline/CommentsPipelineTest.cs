namespace PetSanctuary.Test.Pipeline
{
    using PetSanctuary.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using PetSanctuary.Web.ViewModels.Comments;
    using PetSanctuary.Test.Data;
    using PetSanctuary.Data.Models;
    using System.Linq;

    public class CommentsPipelineTest
    {
        [Fact]
        public void GetBlogShouldReturnDefaultView()
            => MyPipeline
            .Configuration()
            .ShouldMap("/Comments/Blog/TestBlogId")
              .To<CommentsController>(c => c.Blog("TestBlogId", new CommentQueryModel()))
            .Which(controller => controller
               .WithData(BlogCommentsTestData.GetComments(1)))
            .ShouldReturn()
            .View(result => result
                .WithModelOfType<CommentQueryModel>()
                .Passing(model => model.Comments.Count == 1));

        [Fact]
        public void GetVetShouldReturnDefaultView()
           => MyPipeline
           .Configuration()
           .ShouldMap("/Comments/Vet/TestVetId")
             .To<CommentsController>(c => c.Vet("TestVetId", new CommentQueryModel()))
           .Which(controller => controller
              .WithData(VetCommentsTestData.GetComments(1)))
           .ShouldReturn()
           .View(result => result
               .WithModelOfType<CommentQueryModel>()
               .Passing(model => model.Comments.Count == 1));

        [Fact]
        public void GetCreateShouldReturnDefault()
            => MyPipeline
            .Configuration()
            .ShouldMap(request => request
                .WithPath("/Comments/Create")
                .WithUser()
                .WithAntiForgeryToken())
              .To<CommentsController>(c => c.Create())
            .Which()
            .ShouldHave()
             .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View();

        [Theory]
        [InlineData("TestComment1")]
        [InlineData("TestComment2")]
        [InlineData("TestComment3")]
        public void PostCreateForBlogCommentShouldRedirectToProperActionAndShouldHaveValidModelState(string content)
            => MyPipeline
            .Configuration()
            .ShouldMap(request => request
                 .WithMethod(HttpMethod.Post)
                 .WithPath("/Comments/Create/TestBlogId")
                 .WithQuery("type", "Blog")
                 .WithFormFields(new
                 {
                     Content = content
                 })
                 .WithUser()
                 .WithAntiForgeryToken())
            .To<CommentsController>(c => c.Create("TestBlogId", "Blog", new CommentInputModel
            {
                Content = content
            }))
            .Which()
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post)
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldHave()
            .ValidModelState()
            .Data(data => data
               .WithSet<Comment>(data => data.Any()))
            .AndAlso()
            .ShouldReturn()
            .RedirectToAction("Blog", "Comments", new { id = "TestBlogId" });


        [Theory]
        [InlineData("TestComment1")]
        [InlineData("TestComment2")]
        [InlineData("TestComment3")]
        public void PostCreateForVetCommentShouldRedirectToProperActionAndShouldHaveValidModelState(string content)
           => MyPipeline
           .Configuration()
           .ShouldMap(request => request
                .WithMethod(HttpMethod.Post)
                .WithPath("/Comments/Create/TestVetId")
                .WithQuery("type", "Vet")
                .WithFormFields(new
                {
                    Content = content
                })
                .WithUser()
                .WithAntiForgeryToken())
           .To<CommentsController>(c => c.Create("TestVetId", "Vet", new CommentInputModel
           {
               Content = content
           }))
           .Which()
           .ShouldHave()
           .ActionAttributes(attributes => attributes
               .RestrictingForHttpMethod(HttpMethod.Post)
               .RestrictingForAuthorizedRequests())
           .AndAlso()
           .ShouldHave()
           .ValidModelState()
           .Data(data => data
              .WithSet<Comment>(data => data.Any()))
           .AndAlso()
           .ShouldReturn()
           .RedirectToAction("Vet", "Comments", new { id = "TestVetId" });


        [Fact]
        public void GetEditShouldReturnDefaultViewWithDefaultModel()
            => MyPipeline
            .Configuration()
            .ShouldMap(request => request
               .WithPath("/Comments/Edit/1")
               .WithUser()
               .WithAntiForgeryToken())
            .To<CommentsController>(c => c.Edit(1))
            .Which(controller => controller
                .WithData(BlogCommentsTestData.GetComments(1)))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View(result => result.WithModelOfType<CommentInputModel>());

        [Theory]
        [InlineData("EditedComment")]
        public void PostEditForBlogShouldReturnDefaultRedirectToActionAndShouldHaveValidModelState(string content)
            => MyPipeline
            .Configuration()
            .ShouldMap(request => request
               .WithMethod(HttpMethod.Post)
               .WithPath("/Comments/Edit/1")
               .WithQuery("type","Blog")
               .WithFormFields(new
               {
                   Content = content
               })
               .WithUser()
               .WithAntiForgeryToken())
            .To<CommentsController>(c => c.Edit(1, "Blog", new CommentInputModel
            {
                Content = content
            }))
            .Which(controller => controller
              .WithData(BlogCommentsTestData.GetComments(1)))
            .ShouldHave()
             .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post)
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldHave()
            .Data(data => data
               .WithSet<Comment>(data => data
                   .Any(comment => comment.Content == content)))
            .AndAlso()
            .ShouldReturn()
            .RedirectToAction("Blog", "Comments", new { id = "TestBlogId" });

        [Theory]
        [InlineData("EditedComment")]
        public void PostEditForVetShouldReturnDefaultRedirectToActionAndShouldHaveValidModelState(string content)
            => MyPipeline
            .Configuration()
            .ShouldMap(request => request
               .WithMethod(HttpMethod.Post)
               .WithPath("/Comments/Edit/1")
               .WithQuery("type", "Vet")
               .WithFormFields(new
               {
                   Content = content
               })
               .WithUser()
               .WithAntiForgeryToken())
            .To<CommentsController>(c => c.Edit(1, "Vet", new CommentInputModel
            {
                Content = content
            }))
            .Which(controller => controller
              .WithData(VetCommentsTestData.GetComments(1)))
            .ShouldHave()
             .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post)
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldHave()
            .Data(data => data
               .WithSet<Comment>(data => data
                   .Any(comment => comment.Content == content)))
            .AndAlso()
            .ShouldReturn()
            .RedirectToAction("Vet", "Comments", new { id = "TestVetId" });

        [Fact]
        public void GetDeleteForBlogShouldReturnDefaultRedirectToAction()
            => MyPipeline
            .Configuration()
            .ShouldMap(request => request
               .WithPath("/Comments/Delete/1")
               .WithQuery("type", "Blog")
               .WithUser()
               .WithAntiForgeryToken())
            .To<CommentsController>(c => c.Delete(1, "Blog"))
            .Which(controller => controller
               .WithData(BlogCommentsTestData.GetComments(1)))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldHave()
            .Data(data => data
                .WithSet<Comment>(data => !data.Any()))
            .AndAlso()
            .ShouldReturn()
            .RedirectToAction("Blog", "Comments", new { id = "TestBlogId" });


        [Fact]
        public void GetDeleteForVetShouldReturnDefaultRedirectToAction()
            => MyPipeline
            .Configuration()
            .ShouldMap(request => request
               .WithPath("/Comments/Delete/1")
               .WithQuery("type", "Vet")
               .WithUser()
               .WithAntiForgeryToken())
            .To<CommentsController>(c => c.Delete(1, "Vet"))
            .Which(controller => controller
               .WithData(VetCommentsTestData.GetComments(1)))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldHave()
            .Data(data => data
                .WithSet<Comment>(data => !data.Any()))
            .AndAlso()
            .ShouldReturn()
            .RedirectToAction("Vet", "Comments", new { id = "TestVetId" });


    }
}
