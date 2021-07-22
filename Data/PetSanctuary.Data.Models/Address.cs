namespace PetSanctuary.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using PetSanctuary.Data.Common.Models;

    using static PetSanctuary.Common.GlobalConstants.Pet;

    public class Address : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(MaxAddressLength)]
        public string Name { get; set; }

        [Required]
        public int CityId { get; set; }

        public City City { get; set; }
    }
}
