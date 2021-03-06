namespace PetSanctuary.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PetSanctuary.Data.Common.Models;

    using static PetSanctuary.Common.GlobalConstants.Vet;

    public class Vet : BaseDeletableModel<string>
    {
        public Vet()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Likes = new HashSet<Like>();
            this.Dislikes = new HashSet<Dislike>();
            this.VetComments = new HashSet<VetComment>();
        }

        [MaxLength(MaxVetFirstNameLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(MaxVetSurnameLength)]
        public string Surname { get; set; }

        [Required]
        public string Qualification { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int ClinicId { get; set; }

        public Clinic Clinic { get; set; }

        public ICollection<Like> Likes { get; set; }

        public ICollection<Dislike> Dislikes { get; set; }

        public ICollection<VetComment> VetComments { get; set; }
    }
}
