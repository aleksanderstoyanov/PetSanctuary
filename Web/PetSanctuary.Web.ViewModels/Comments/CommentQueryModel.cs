namespace PetSanctuary.Web.ViewModels.Comments
{
    using System.Collections.Generic;

    using PetSanctuary.Services.Data.Comments;
    using PetSanctuary.Web.ViewModels.User;

    public class CommentQueryModel : BaseQueryModel
    {
        public ICollection<CommentServiceModel> Comments { get; set; }
    }
}
