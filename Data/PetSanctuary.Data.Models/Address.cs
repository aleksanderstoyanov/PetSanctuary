using PetSanctuary.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using PetSanctuary.Common;

namespace PetSanctuary.Data.Models
{

    public class Address : BaseDeletableModel<int>
    {

        [Required]
        [MaxLength(GlobalConstants.MaxAddressLength)]
        public string Name { get; set; }

        [Required]
        public int CityId { get; set; }

        public City City { get; set; }
    }
}
