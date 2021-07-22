namespace PetSanctuary.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using PetSanctuary.Data.Common.Models;

    using static PetSanctuary.Common.GlobalConstants.Pet;

    public class City : BaseDeletableModel<int>
    {
        public City()
        {
            this.Addresses = new HashSet<Address>();
        }

        [Required]
        [MaxLength(MaxCityLength)]
        public string Name { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}
