namespace PetSanctuary.Services.Data.Clinics
{
    using System.Collections.Generic;

    public interface IClinicService
    {
        IEnumerable<ClinicServiceModel> GetAllClinics();

        ClinicServiceModel GetClinicById(int id);

        ClinicServiceModel GetClinicByName(string name);
    }
}
