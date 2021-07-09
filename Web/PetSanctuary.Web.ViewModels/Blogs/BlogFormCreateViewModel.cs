using PetSanctuary.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;




namespace PetSanctuary.Web.ViewModels.Blogs
{
    public class BlogFormCreateViewModel
    {
        [Required]
        [MinLength(GlobalConstants.MinBlogTitleLength)]
        [MaxLength(GlobalConstants.MaxBlogTitleLength)]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Image-Url")]
        [Url]
        public string Image { get; set; }
        [Required]
        [MinLength(GlobalConstants.MinBlogDescriptionLength)]
        [MaxLength(GlobalConstants.MaxBlogDescriptionLength)]
        public string Description { get; set; }

    }
}
