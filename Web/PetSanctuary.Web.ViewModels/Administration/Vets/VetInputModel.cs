namespace PetSanctuary.Web.ViewModels.Administration.Vets
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using PetSanctuary.Common;

    using static PetSanctuary.Common.GlobalConstants.Vet;

    public class VetInputModel
    {
        [StringLength(
            MaxVetFirstNameLength,
            MinimumLength = MinVetFirstNameLength,
            ErrorMessage = "Name should be between 4 and 30 characters")]
        public string FirstName { get; set; }

        [StringLength(
           MaxVetSurnameLength,
           MinimumLength = MinVetSurnameLength,
           ErrorMessage = "Name should be between 5 and 30 characters")]
        public string Surname { get; set; }

        [StringLength(
         MaxDescriptionLength,
         MinimumLength = MinDecriptionLength,
         ErrorMessage = "Name should be between 5 and 250 characters")]
        public string Description { get; set; }

        [StringLength(
        MaxQualificationLength,
        MinimumLength = MinQualifactionLength,
        ErrorMessage = "Name should be between 4 and 20 characters")]
        public string Qualification { get; set; }

        public string Clinic { get; set; }
    }
}
