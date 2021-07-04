using System;
using System.Collections.Generic;
using System.Text;

namespace PetSanctuary.Web.ViewModels.Catalog
{
    public class CatalogDetailsViewModel
    {
        public string Id { get; set; }
        public string Image { get; set; }
        public string CreatedOn { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string IsVaccinated { get; set; }
    }
}
