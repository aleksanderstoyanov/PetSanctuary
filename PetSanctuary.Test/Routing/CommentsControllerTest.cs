namespace PetSanctuary.Test.Routing
{
    using PetSanctuary.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using PetSanctuary.Web.ViewModels.Comments;

    public class CommentsControllerTest
    {
        [Fact]
        public void BlogRouteShouldBeMappedCorrectly()
            => MyRouting
            .Configuration()
            .ShouldMap("/Comments/Blog/Test")
            .To<CommentsController>(c => c
               .Blog("Test", new CommentQueryModel()));

        [Fact]
        public void VetRouteShouldBeMappedCorrectly()
             => MyRouting
            .Configuration()
            .ShouldMap("/Comments/Vet/Test")
            .To<CommentsController>(c => c
              .Vet("Test", new CommentQueryModel()));

        [Fact]
        public void GetCreateShouldBeMappedCorrectly()
            => MyRouting
            .Configuration()
            .ShouldMap(request => request
                 .WithPath("/Comments/Create/Test")
                 .WithMethod(HttpMethod.Get))
            .To<CommentsController>(c => c
                .Create());

        [Fact]
        public void PostCreateShouldBeMappedCorrectly()
            => MyRouting
            .Configuration()
            .ShouldMap(request => request
             .WithPath("/Comments/Create/Test")
             .WithMethod(HttpMethod.Post)
             .WithAntiForgeryToken())
            .To<CommentsController>(c => c
              .Create());

        [Fact]
        public void GetEditShouldBeMappedCorrectly()
            => MyRouting
            .Configuration()
            .ShouldMap(request => request
              .WithPath("/Comments/Edit/1")
            .WithMethod(HttpMethod.Get))
            .To<CommentsController>(c => c
                .Edit(1));

        [Fact]
        public void PostEditForBlogShouldBeMappedCorrectly()
            => MyRouting
            .Configuration()
            .ShouldMap(request => request
               .WithPath("/Comments/Edit/1")
               .WithMethod(HttpMethod.Post)
               .WithAntiForgeryToken()
               .WithQuery("type", "Blog"))
            .To<CommentsController>(c => c
                 .Edit(1, "Blog", new CommentInputModel()));

        [Fact]
        public void PostEditForVetShouldBeMappedCorrectly()
            => MyRouting
            .Configuration()
            .ShouldMap(request => request
                 .WithPath("/Comments/Edit/1")
                 .WithMethod(HttpMethod.Post)
                 .WithAntiForgeryToken()
                 .WithQuery("type", "Vet"))
            .To<CommentsController>(c => c
                .Edit(1, "Vet", new CommentInputModel()));

        [Fact]
        public void DeleteForBlogShouldBeMappedCorrectly()
            => MyRouting
            .Configuration()
            .ShouldMap(request => request
                 .WithPath("/Comments/Delete/1")
                 .WithQuery("type", "Blog"))
            .To<CommentsController>(c => c
                  .Delete(1, "Blog"));

        [Fact]
        public void DeleteForVetShouldBeMappedCorrectly()
            => MyRouting
            .Configuration()
            .ShouldMap(request => request
                  .WithPath("/Comments/Delete/1")
                  .WithQuery("type", "Vet"))
            .To<CommentsController>(c => c
               .Delete(1, "Vet"));


    }
}
