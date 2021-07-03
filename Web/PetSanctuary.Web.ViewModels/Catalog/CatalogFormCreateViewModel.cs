using System;
using System.Collections.Generic;
using System.Text;

namespace PetSanctuary.Web.ViewModels.Catalog
{
    public class CatalogFormCreateViewModel
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string Image { get; set; }

        public string Type { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string IsVaccinated { get; set; }
    }
}
