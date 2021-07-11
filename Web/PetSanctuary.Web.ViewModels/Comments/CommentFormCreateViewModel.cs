using PetSanctuary.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetSanctuary.Web.ViewModels.Comments
{
   public class CommentFormCreateViewModel
    {
        [Required]
        [MinLength(GlobalConstants.MinCommentContentLength)]
        [MaxLength(GlobalConstants.MaxCommentContentLength)]
        public string Content { get; set; }
    }
}
