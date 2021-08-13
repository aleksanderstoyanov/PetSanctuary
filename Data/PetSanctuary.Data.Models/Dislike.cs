namespace PetSanctuary.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using PetSanctuary.Data.Common.Models;

    public class Dislike : BaseDeletableModel<int>
    {
        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
