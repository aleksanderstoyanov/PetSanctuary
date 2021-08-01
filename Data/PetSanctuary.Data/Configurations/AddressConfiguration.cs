namespace PetSanctuary.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PetSanctuary.Data.Models;

    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> address)
        {
            address
            .HasOne(e => e.City)
            .WithMany(e => e.Addresses)
            .HasForeignKey(e => e.CityId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
