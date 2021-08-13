using MyTested.AspNetCore.Mvc;
using PetSanctuary.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace PetSanctuary.Test.Data
{
    public class BlogTestData
    {
        public static List<Blog> GetBlogs(int count)
        {
            var blogs =
                Enumerable
                .Range(1, count)
                .Select(i => new Blog
                {
                    Id = "TestBlogId"+i,
                    Title = "TestBlogTitle",
                    Description = "Test description blog",
                    Image = "TestImage",
                    Author = new ApplicationUser
                    {
                        Id = TestUser.Identifier,
                        UserName = TestUser.Username
                    }
                }).ToList();

            return blogs;
        }
    }
}
