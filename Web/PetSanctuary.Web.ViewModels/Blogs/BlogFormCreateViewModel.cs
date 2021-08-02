namespace PetSanctuary.Web.ViewModels.Blogs
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

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
        public IFormFile Image { get; set; }

        [Required]
        [StringLength(
         MaxBlogDescriptionLength,
         MinimumLength = MinBlogDescriptionLength,
         ErrorMessage = "Field description should be between 10 and 200")]
        public string Description { get; set; }
    }
}
