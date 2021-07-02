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
    public class UserPetConfiguration : IEntityTypeConfiguration<UserPet>
    {
        public void Configure(EntityTypeBuilder<UserPet> userPet)
        {

            userPet
                .HasKey(e => new { e.UserId, e.PetId });
        }
    }
}
