using PetSanctuary.Services.Data.Blogs;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetSanctuary.Web.ViewModels.User
{
    public class BlogPostQueryModel : BaseQueryModel
    {
        public ICollection<BlogServiceModel> Blogs { get; set; }
    }
}
