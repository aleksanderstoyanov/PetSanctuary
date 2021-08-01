namespace PetSanctuary.Web.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;

    using static PetSanctuary.Common.GlobalConstants.Blog;

    public class BlogPostViewModel
    {
        public string Id { get; set; }

        [Required]
        [Url]
        [Display(Name = "Image-Url")]

        public string Image { get; set; }

        [Required]
        [StringLength(
         MaxBlogTitleLength,
         MinimumLength = MinBlogTitleLength,
         ErrorMessage = "Field should title be between 3 and 20")]
        public string Title { get; set; }

        [Required]
        [StringLength(
         MaxBlogDescriptionLength,
         MinimumLength = MinBlogDescriptionLength,
         ErrorMessage = "Field should be between 10 and 200")]
        public string Description { get; set; }
    }
}
