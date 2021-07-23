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
    public class VetCommentConfiguration : IEntityTypeConfiguration<VetComment>
    {
        public void Configure(EntityTypeBuilder<VetComment> vetComment)
        {
            vetComment
                .HasKey(x => new { x.VetId, x.CommentId });
        }
    }
}
