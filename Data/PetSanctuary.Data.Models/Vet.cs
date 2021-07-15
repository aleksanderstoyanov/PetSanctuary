using PetSanctuary.Common;
using PetSanctuary.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetSanctuary.Data.Models
{
    public class Vet : BaseDeletableModel<string>
    {
        public Vet()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [MaxLength(GlobalConstants.MaxVetFirstNameLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(GlobalConstants.MaxVetSurnameLength)]
        public string Surname { get; set; }

        [Required]
        public string Qualification { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int ClinicId { get; set; }

        public Clinic Clinic { get; set; }

    }
}
