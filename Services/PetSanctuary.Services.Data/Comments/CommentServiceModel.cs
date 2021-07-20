using System;
using System.Collections.Generic;
using System.Text;

namespace PetSanctuary.Services.Data.Comments
{
    public class CommentServiceModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string BlogId { get; set; }

        public string PublishedOn { get; set; }

        public string Publisher { get; set; }

    }
}
