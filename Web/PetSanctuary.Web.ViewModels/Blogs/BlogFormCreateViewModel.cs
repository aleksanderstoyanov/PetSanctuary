namespace PetSanctuary.Web.ViewModels.Blogs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using static PetSanctuary.Common.GlobalConstants.Blog;

    public class BlogFormCreateViewModel
    {
        [Required]
        [StringLength(
         MaxBlogTitleLength,
         MinimumLength = MinBlogTitleLength,
         ErrorMessage = "Field title should be between 3 and 20")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Image-Url")]
        [Url]
        public string Image { get; set; }

        [Required]
        [StringLength(
         MaxBlogDescriptionLength,
         MinimumLength = MinBlogDescriptionLength,
         ErrorMessage = "Field description should be between 10 and 200")]
        public string Description { get; set; }

    }
}
