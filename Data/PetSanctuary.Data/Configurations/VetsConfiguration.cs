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
    public class VetsConfiguration : IEntityTypeConfiguration<Vet>
    {
        public void Configure(EntityTypeBuilder<Vet> vets)
        {
            vets.
                 HasOne(x => x.Clinic)
                 .WithMany(x => x.Vets);
        }
    }
}
