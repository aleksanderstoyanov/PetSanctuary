namespace PetSanctuary.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class BlogComment
    {
        [Required]
        public string BlogId { get; set; }

        public Blog Blog { get; set; }

        [Required]
        public int CommentId { get; set; }

        public Comment Comment { get; set; }
    }
}
