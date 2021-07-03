using PetSanctuary.Data.Common.Models;
using PetSanctuary.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetSanctuary.Data.Models
{
    public class Pet : BaseDeletableModel<string>
    {
        public Pet()
        {
            this.Id = Guid.NewGuid().ToString();
            this.UserPets = new HashSet<UserPet>();
        }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public int? Age { get; set; }

        public string Image { get; set; }

        [Required]
        public bool IsVaccinated { get; set; }

        public PetType Type { get; set; }

        public string OwnerId { get; set; }

        [Required]
        public int CityId { get; set; }

        public City City { get; set; }

        [Required]
        public int AddressId { get; set; }

        public Address Address { get; set; }

        public ICollection<UserPet> UserPets { get; set; }
    }
}
