namespace PetSanctuary.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using PetSanctuary.Data.Common.Models;

    using static PetSanctuary.Common.GlobalConstants.Blog;

    public class Blog : BaseDeletableModel<string>
    {
        public Blog()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Comments = new HashSet<Comment>();
        }

        [Required]
        [MaxLength(MaxBlogTitleLength)]
        public string Title { get; set; }

        public string Image { get; set; }

        [Required]
        [MaxLength(MaxBlogDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }

        public ICollection<Comment> Comments { get; set; }

    }
}
