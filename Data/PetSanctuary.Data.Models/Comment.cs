namespace PetSanctuary.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using PetSanctuary.Data.Common.Models;

    using static PetSanctuary.Common.GlobalConstants.Blog;

    public class Comment : BaseDeletableModel<int>
    {

        [MaxLength(MaxCommentContentLength)]
        [Required]
        public string Content { get; set; }

        [Required]
        public string PublisherId { get; set; }

        public ApplicationUser Publisher { get; set; }

        [Required]
        public string BlogId { get; set; }

        public Blog Blog { get; set; }

        [Required]
        public DateTime PublishedOn { get; set; }

    }
}
