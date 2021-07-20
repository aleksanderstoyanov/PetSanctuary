using System;
using System.Collections.Generic;
using System.Text;

namespace PetSanctuary.Services.Data.Blogs
{
    public class BlogServiceModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public string CreatedOn { get; set; }
    }
}
