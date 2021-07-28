using PetSanctuary.Services.Data.Catalogs;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetSanctuary.Web.ViewModels.User
{
    public class PetPostQueryModel : BaseQueryModel
    {
        public ICollection<CatalogServiceModel> Pets { get; set; }
    }
}
