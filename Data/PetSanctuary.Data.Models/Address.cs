using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetSanctuary.Data.Models
{
    public class Address
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int CityId { get; set; }

        public City City { get; set; }
    }
}
