using PetSanctuary.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetSanctuary.Web.ViewModels.User
{
    public class PetPostViewModel
    {
        public string Id { get; set; }
        [Required]
        [MinLength(GlobalConstants.MinPetNameLength)]
        [MaxLength(GlobalConstants.MaxPetNameLength)]
        public string Name { get; set; }
        [Required]
        [Range(1, 18)]
        public int Age { get; set; }
        [Required]
        
        public string Type { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [Display(Name = "Image-Url")]
        [Url]
        public string Image { get; set; }
        [Required]
        [MinLength(GlobalConstants.MinCityLength)]
        [MaxLength(GlobalConstants.MaxCityLength)]
        public string City { get; set; }
        [Required]
        [MinLength(GlobalConstants.MinAddressLength)]
        [MaxLength(GlobalConstants.MaxAddressLength)]
        public string Address { get; set; }
        [Required]
        public string IsVaccinated { get; set; }
    }
}
