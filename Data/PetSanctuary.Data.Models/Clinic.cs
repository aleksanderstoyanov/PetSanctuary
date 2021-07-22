namespace PetSanctuary.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using PetSanctuary.Data.Common.Models;

    using static PetSanctuary.Common.GlobalConstants.Clinic;

    public class Clinic : BaseDeletableModel<int>
    {
        public Clinic()
        {
            this.Vets = new HashSet<Vet>();
        }

        [Required]
        [MaxLength(MaxClinicNameLength)]
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
