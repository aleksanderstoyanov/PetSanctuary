using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetSanctuary.Data.Models
{
    public class City
    {
        public City()
        {
            this.Addresses = new HashSet<Address>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}
