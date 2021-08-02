namespace PetSanctuary.Web.ViewModels.Clinics
{
    using System.Collections.Generic;

    using PetSanctuary.Services.Data.Clinics;
    using PetSanctuary.Web.ViewModels.User;

    public class ClinicsQueryModel : BaseQueryModel
    {
        public string City { get; set; }

        public ICollection<ClinicServiceModel> Clinics { get; set; }
    }
}
