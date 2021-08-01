namespace PetSanctuary.Web.ViewModels.Catalog
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.ComponentModel.DataAnnotations;

    using static PetSanctuary.Common.GlobalConstants.Pet;

    public class CatalogFormCreateViewModel
    {
        [Required]
        [StringLength(
         MaxPetNameLength,
         MinimumLength = MinPetNameLength,
         ErrorMessage = "Field name should be between 3 and 15")]
        public string Name { get; set; }

        [Required]
        [Range(1, 18, ErrorMessage = "Age should be between 1 and 18")]
        public int Age { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [StringLength(
         MaxCityLength,
         MinimumLength = MinCityLength,
         ErrorMessage = "Field city should be between 4 and 20")]
        public string City { get; set; }

        [Required]
        [StringLength(
         MaxAddressLength,
         MinimumLength = MinAddressLength,
         ErrorMessage = "Field should be between 4 and 30")]
        public string Address { get; set; }

        [Required]
        public string IsVaccinated { get; set; }
    }
}
