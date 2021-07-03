using PetSanctuary.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetSanctuary.Data.Models
{
    public class Blog:BaseDeletableModel<string>
    {
        public Blog()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }
    }
}
