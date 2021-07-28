namespace PetSanctuary.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using PetSanctuary.Data.Common.Models;
    using PetSanctuary.Data.Models.Enums;

    using static PetSanctuary.Common.GlobalConstants.Pet;

    public class Pet : BaseDeletableModel<string>
    {
        public Pet()
        {
            this.Id = Guid.NewGuid().ToString();
            this.UserPets = new HashSet<UserPet>();
        }

        [Required]
        [MaxLength(MaxPetNameLength)]
        public string Name { get; set; }

        public int Age { get; set; }

        [Required]
        public GenderType Gender { get; set; }

        public string Image { get; set; }

        [Required]
        public bool IsVaccinated { get; set; }

        public PetType Type { get; set; }

        public string OwnerId { get; set; }

        public ApplicationUser Owner { get; set; }

        [Required]
        public int CityId { get; set; }

        public City City { get; set; }

        [Required]
        public int AddressId { get; set; }

        public Address Address { get; set; }

        public ICollection<UserPet> UserPets { get; set; }
    }
}
