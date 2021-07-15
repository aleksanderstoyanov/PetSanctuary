namespace PetSanctuary.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using PetSanctuary.Data.Models;

    public class ClinicsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Clinics.Any())
            {
                return;
            }

            await dbContext.Clinics.AddAsync(new Clinic
            {
                Name = "SinKrust",
                CityId = 1,
                AddressId = 6,
                Image = "https://pomorievetclinic.com/wp-content/uploads/2015/11/Klinika-Vhod.jpg"

            });
            await dbContext.Clinics.AddAsync(new Clinic
            {
                Name = "Izgrev",
                CityId = 17,
                AddressId = 2,
                Image = "https://nacionalen-biznes.com/custom/domain_1/image_files/sitemgr_photo_129496.jpg"

            });

            await dbContext.Clinics.AddAsync(new Clinic
            {
                Name = "Izgrev",
                CityId = 16,
                AddressId = 4,
                Image = "https://bgregistar.com/res/objects/14918/page/16_V%D0%B5t%D0%B5rinarna%20klinika%20DianaV%D0%B5t.jpg"

            });
        }
    }
}
