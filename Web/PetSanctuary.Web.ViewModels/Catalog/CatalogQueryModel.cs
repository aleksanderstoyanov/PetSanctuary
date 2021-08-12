namespace PetSanctuary.Web.ViewModels.Catalog
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using PetSanctuary.Web.ViewModels.User;

    public class CatalogQueryModel : BaseQueryModel
    {
        public string Type { get; set; } = "Dog";

        public ICollection<CatalogViewModel> Pets { get; set; }
    }
}
