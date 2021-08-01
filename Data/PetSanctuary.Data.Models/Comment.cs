namespace PetSanctuary.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PetSanctuary.Data.Common.Models;

    using static PetSanctuary.Common.GlobalConstants.Blog;

    public class Comment : BaseDeletableModel<int>
    {
        public Comment()
        {
            this.BlogComments = new HashSet<BlogComment>();
            this.VetComments = new HashSet<VetComment>();
        }

        [MaxLength(MaxCommentContentLength)]
        [Required]
        public string Content { get; set; }

        [Required]
        public string PublisherId { get; set; }

        public ApplicationUser Publisher { get; set; }

        [Required]
        public DateTime PublishedOn { get; set; }

        public ICollection<BlogComment> BlogComments { get; set; }

        public ICollection<VetComment> VetComments { get; set; }
    }
}
