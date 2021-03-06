namespace PetSanctuary.Web.ViewModels.Administration.Vets
{
    using System.ComponentModel.DataAnnotations;

    using static PetSanctuary.Common.GlobalConstants.Vet;

    public class VetInputModel
    {
        [Required]
        [StringLength(
            MaxVetFirstNameLength,
            MinimumLength = MinVetFirstNameLength,
            ErrorMessage = "Name should be between 4 and 30 characters")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(
           MaxVetSurnameLength,
           MinimumLength = MinVetSurnameLength,
           ErrorMessage = "Name should be between 5 and 30 characters")]
        public string Surname { get; set; }

        [Required]
        [StringLength(
         MaxDescriptionLength,
         MinimumLength = MinDecriptionLength,
         ErrorMessage = "Name should be between 5 and 250 characters")]
        public string Description { get; set; }

        [Required]
        [StringLength(
        MaxQualificationLength,
        MinimumLength = MinQualifactionLength,
        ErrorMessage = "Name should be between 4 and 20 characters")]
        public string Qualification { get; set; }

        [Required]
        public string Clinic { get; set; }
    }
}
