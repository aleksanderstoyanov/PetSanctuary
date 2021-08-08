
namespace PetSanctuary.Test.Data
{
    using PetSanctuary.Data.Models;
    using System.Collections.Generic;
    using MyTested.AspNetCore.Mvc;
    using System.Linq;

    public class BlogCommentsTestData
    {
        public static List<Comment> GetComments(int count)
        {
            var blog = new Blog
            {
                Id = "TestBlogId",
                Title = "TestTitle",
                Description = "Test description blog",
                Image = "TestImage"
            };

            var publisher = new ApplicationUser
            {
                Id = TestUser.Identifier,
                UserName = TestUser.Username
            };

            var comments = Enumerable
                .Range(1, count)
                .Select(i => new Comment
                {
                    Id = i,
                    Content = "Comment test content" + i,
                    Publisher = publisher

                }).ToList();

            foreach (var comment in comments)
            {
                comment.BlogComments.Add(new BlogComment
                {
                    Blog=blog,
                    Comment=comment
                });
            };


            return comments;
        }
    }
}
