using PetSanctuary.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetSanctuary.Web.ViewModels.Comments
{
    public class CommentViewModel
    {
        [MinLength(GlobalConstants.MinCommentContentLength)]
        [MaxLength(GlobalConstants.MaxCommentContentLength)]
        [Required]
        public string Content { get; set; }

        [Required]
        public string PublishedOn { get; set; }

        [Required]
        public string Publisher { get; set; }

    }
}
