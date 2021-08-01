namespace PetSanctuary.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PetSanctuary.Data.Models;

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
