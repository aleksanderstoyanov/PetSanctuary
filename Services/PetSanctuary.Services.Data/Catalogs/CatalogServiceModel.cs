using System;
using System.Collections.Generic;
using System.Text;

namespace PetSanctuary.Services.Data.Catalogs
{
    public class CatalogServiceModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Type { get; set; }

        public string Image { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        public string City { get; set; }

        public string CreatedOn { get; set; }

        public string IsVaccinated { get; set; }

        public string PhoneNumber { get; set; }
    }
}
