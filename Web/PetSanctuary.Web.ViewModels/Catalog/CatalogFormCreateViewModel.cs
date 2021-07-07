using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetSanctuary.Web.ViewModels.Catalog
{
    public class CatalogFormCreateViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, 18)]
        public int Age { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string Type { get; set; }
        [Required]
        public string Gender { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string IsVaccinated { get; set; }
    }
}
