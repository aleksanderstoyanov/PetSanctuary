using PetSanctuary.Common;
using PetSanctuary.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetSanctuary.Data.Models
{
    public class Blog : BaseDeletableModel<string>
    {
        public Blog()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Comments = new HashSet<Comment>();
        }

        [Required]
        [MaxLength(GlobalConstants.MaxBlogTitleLength)]
        public string Title { get; set; }
        public string Image { get; set; }

        [Required]
        [MaxLength(GlobalConstants.MaxBlogDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}
