namespace PetSanctuary.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PetSanctuary.Data.Models;

    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> blog)
        {
            blog
             .HasOne(e => e.Author)
             .WithMany(e => e.Blogs)
             .HasForeignKey(e => e.AuthorId)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
