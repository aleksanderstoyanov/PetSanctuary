namespace PetSanctuary.Web.ViewModels.Administration.Clinics
{
    using System.ComponentModel.DataAnnotations;

    using PetSanctuary.Common;

    public class ClinicInputModel
    {
        [Required]
        [StringLength(
         GlobalConstants.Clinic.MaxClinicNameLength,
         MinimumLength = GlobalConstants.Clinic.MinClinicNameLength,
         ErrorMessage = "Clinic name should be between 3 and 30 symbols")]
        public string Name { get; set; }

        [Required]
        [StringLength(
         GlobalConstants.Pet.MaxAddressLength,
         MinimumLength = GlobalConstants.Pet.MinAddressLength,
         ErrorMessage = "Clinic name should be between 4 and 30 symbols")]
        public string Address { get; set; }

        [Required]
        [StringLength(
        GlobalConstants.Pet.MaxCityLength,
        MinimumLength = GlobalConstants.Pet.MinCityLength,
        ErrorMessage = "Clinic name should be between 4 and 20 symbols")]
        public string City { get; set; }

        [Required]
        [Url]
        public string Image { get; set; }
    }
}
