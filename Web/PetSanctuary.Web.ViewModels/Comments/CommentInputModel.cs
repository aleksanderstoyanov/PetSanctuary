namespace PetSanctuary.Web.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using static PetSanctuary.Common.GlobalConstants.Blog;

    public class CommentInputModel
    {
        [Required]
        [StringLength(
         MaxCommentContentLength,
         MinimumLength = MinCommentContentLength,
         ErrorMessage = "Field content should be between 3 and 90")]
        public string Content { get; set; }
    }
}
