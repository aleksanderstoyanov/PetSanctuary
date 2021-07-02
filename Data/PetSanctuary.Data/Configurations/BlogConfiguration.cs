using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetSanctuary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSanctuary.Data.Configurations
{
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
