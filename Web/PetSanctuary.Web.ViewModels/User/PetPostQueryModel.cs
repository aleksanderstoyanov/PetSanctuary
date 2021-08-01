namespace PetSanctuary.Web.ViewModels.User
{
    using System.Collections.Generic;

    using PetSanctuary.Services.Data.Catalogs;

    public class PetPostQueryModel : BaseQueryModel
    {
        public ICollection<CatalogServiceModel> Pets { get; set; }
    }
}
