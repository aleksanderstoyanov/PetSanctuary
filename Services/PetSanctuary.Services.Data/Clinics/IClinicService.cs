namespace PetSanctuary.Services.Data.Clinics
{
    using System.Collections.Generic;

    public interface IClinicService
    {
        IEnumerable<ClinicServiceModel> GetAllClinics();

        IEnumerable<ClinicServiceModel> GetAllClinicsByCity(string city, int currentPage, int postsPerPage);

        ClinicServiceModel GetClinicById(int id);

        ClinicServiceModel GetClinicByName(string name);
    }
}
