using PetSanctuary.Common;
using PetSanctuary.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetSanctuary.Data.Models
{
    public class Comment : BaseDeletableModel<int>
    {

        [MaxLength(GlobalConstants.MaxCommentContentLength)]
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
