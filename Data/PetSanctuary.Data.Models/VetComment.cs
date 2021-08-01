namespace PetSanctuary.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class VetComment
    {
        [Required]
        public string VetId { get; set; }

        public Vet Vet { get; set; }

        [Required]
        public int CommentId { get; set; }

        public Comment Comment { get; set; }
    }
}
