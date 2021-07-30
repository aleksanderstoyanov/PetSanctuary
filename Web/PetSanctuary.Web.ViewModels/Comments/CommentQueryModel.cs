using PetSanctuary.Services.Data.Comments;
using PetSanctuary.Web.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetSanctuary.Web.ViewModels.Comments
{
    public class CommentQueryModel : BaseQueryModel
    {
        public ICollection<CommentServiceModel> Comments { get; set; }
    }
}
