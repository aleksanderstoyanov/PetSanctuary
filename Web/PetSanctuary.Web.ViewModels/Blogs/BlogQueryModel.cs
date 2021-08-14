namespace PetSanctuary.Web.ViewModels.Blogs
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using PetSanctuary.Services.Data.Blogs;
    using PetSanctuary.Web.ViewModels.User;

    public class BlogQueryModel : BaseQueryModel
    {
        public ICollection<BlogServiceModel> Blogs { get; set; }
    }
}
