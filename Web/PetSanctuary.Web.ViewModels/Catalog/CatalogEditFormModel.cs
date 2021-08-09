namespace PetSanctuary.Web.ViewModels.Catalog
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    using static PetSanctuary.Common.GlobalConstants.Pet;

    public class CatalogEditFormModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(
         MaxPetNameLength,
         MinimumLength = MinPetNameLength,
         ErrorMessage = "The field name should be with minimum length 3 and maximum length 15")]
        public string Name { get; set; }

        [Required]
        [Range(1, 18, ErrorMessage = "Age should be between 1 and 18")]
        public int Age { get; set; }

        [Required]

        public string Type { get; set; }

        [Required]
        public string Gender { get; set; }

        public IFormFile Image { get; set; }

        [Required]
        [StringLength(
         MaxCityLength,
         MinimumLength = MinCityLength,
         ErrorMessage = "Field city should be with a length between 4 and 20")]
        public string City { get; set; }

        [Required]
        [StringLength(
         MaxAddressLength,
         MinimumLength = MinAddressLength,
         ErrorMessage = "Field address should be 4 and 30")]
        public string Address { get; set; }

        [Required]
        public string IsVaccinated { get; set; }

        public string PhoneNumber { get; set; }
    }
}
