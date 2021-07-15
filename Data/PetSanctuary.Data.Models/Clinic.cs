using PetSanctuary.Common;
using PetSanctuary.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetSanctuary.Data.Models
{
    public class Clinic : BaseDeletableModel<int>
    {
        public Clinic()
        {
            this.Vets = new HashSet<Vet>();
        }

        [Required]
        [MaxLength(GlobalConstants.MaxClinicNameLength)]
        public string Name { get; set; }

        [Required]
        public int CityId { get; set; }

        public City City { get; set; }

        [Required]
        public int AddressId { get; set; }

        [Url]
        public string Image { get; set; }

        public ICollection<Vet> Vets { get; set; }


    }
}
