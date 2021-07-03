using PetSanctuary.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetSanctuary.Data.Models
{
    public class City : BaseDeletableModel<int>
    {
        public City()
        {
            this.Addresses = new HashSet<Address>();
        }

        [Required]
        public string Name { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}
