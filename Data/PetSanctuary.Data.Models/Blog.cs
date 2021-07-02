using System;
using System.Collections.Generic;
using System.Text;

namespace PetSanctuary.Data.Models
{
    public class Blog
    {
        public Blog()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }
    }
}
