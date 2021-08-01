namespace PetSanctuary.Web.ViewModels.User
{
    using System.Collections.Generic;

    using PetSanctuary.Services.Data.Blogs;

    public class BlogPostQueryModel : BaseQueryModel
    {
        public ICollection<BlogServiceModel> Blogs { get; set; }
    }
}
