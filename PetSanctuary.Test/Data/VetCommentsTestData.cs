
namespace PetSanctuary.Test.Data
{
    using PetSanctuary.Data.Models;
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    public class VetCommentsTestData
    {
        public static List<Comment> GetComments(int count)
        {
            var clinic = new Clinic
            {
                Id = 1,
                Name = "Test",
                City = new City
                {
                    Name = "TestCity"
                },
                Address = new Address
                {
                    Name = "TestAddress",
                    City = new City
                    {
                        Name = "TestCity"
                    }
                },
                Image = "TestImage"

            };

            var vet = new Vet
            {
                Id = "TestVetId",
                FirstName = "FirstName",
                Surname = "Surname",
                Qualification = "Veterinary",
                Description = "Test description",
                Clinic = clinic
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
                comment.VetComments.Add(new VetComment
                {
                    Vet = vet,
                    Comment = comment
                });
            };


            return comments;
        }
    }
}
