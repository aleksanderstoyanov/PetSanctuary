namespace PetSanctuary.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PetSanctuary.Data.Models;

    public class UserPetConfiguration : IEntityTypeConfiguration<UserPet>
    {
        public void Configure(EntityTypeBuilder<UserPet> userPet)
        {
            userPet
                .HasKey(e => new { e.UserId, e.PetId });
        }
    }
}
