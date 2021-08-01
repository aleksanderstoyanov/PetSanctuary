namespace PetSanctuary.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PetSanctuary.Data.Models;

    public class VetCommentConfiguration : IEntityTypeConfiguration<VetComment>
    {
        public void Configure(EntityTypeBuilder<VetComment> vetComment)
        {
            vetComment
                .HasKey(x => new { x.VetId, x.CommentId });
        }
    }
}
