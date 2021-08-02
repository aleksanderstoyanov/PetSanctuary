namespace PetSanctuary.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using PetSanctuary.Services.Data.Clinics;
    using PetSanctuary.Services.Data.Comments;
    using PetSanctuary.Services.Data.Vets;
    using PetSanctuary.Web.ViewModels.Clinics;
    using PetSanctuary.Web.ViewModels.Vets;

    public class ClinicsController : BaseController
    {
        private readonly IClinicService clinicService;
        private readonly IVetService vetService;
        private readonly ICommentService commentService;

        public ClinicsController(IClinicService clinicService, IVetService vetService, ICommentService commentService)
        {
            this.clinicService = clinicService;
            this.vetService = vetService;
            this.commentService = commentService;
        }

        public IActionResult Index([FromQuery]ClinicsQueryModel query)
        {
            var clinics = this.clinicService.GetAllClinicsByCity(query.City).ToList();
            query.Clinics = clinics;
            return this.View(query);
        }

        public IActionResult Vets(int id)
        {
            var model = this.vetService.GetVetsById(id)
                .Select(x => new VetsByIdViewModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    Surname = x.Surname,
                    Likes = x.Likes == null ? 0 : x.Likes,
                    Dislikes = x.Dislikes == null ? 0 : x.Dislikes,
                }).ToList();
            return this.View(model);
        }

        public async Task<IActionResult> Like(string vetId)
        {
            var vet = this.vetService.GetVetById(vetId);
            await this.vetService.UpdateLikesAsync(vetId);
            return this.RedirectToAction(nameof(this.Vets), "Clinics", new { id = vet.ClinicId });
        }

        public async Task<IActionResult> Dislike(string vetId)
        {
            var vet = this.vetService.GetVetById(vetId);
            await this.vetService.UpdateDislikesAsync(vetId);
            return this.RedirectToAction(nameof(this.Vets), "Clinics", new { id = vet.ClinicId });
        }

        public IActionResult Description(string id)
        {
            var vet = this.vetService.GetVetById(id);
            var model = new VetsDetailViewModel
            {
                FirstName = vet.FirstName,
                Surname = vet.Surname,
                Description = vet.Description,
            };

            return this.View(model);
        }
    }
}
