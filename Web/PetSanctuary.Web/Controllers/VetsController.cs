namespace PetSanctuary.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PetSanctuary.Services.Data.Vets;
    using PetSanctuary.Web.ViewModels.Vets;

    public class VetsController : BaseController
    {
        private readonly IVetService vetService;

        public VetsController(IVetService vetService)
        {
            this.vetService = vetService;
        }

        public IActionResult Index(int id)
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

        [Authorize]
        public async Task<IActionResult> Like(string id)
        {
            var vet = this.vetService.GetVetById(id);
            await this.vetService.UpdateLikesAsync(id);
            return this.RedirectToAction(nameof(this.Index), "Vets", new { id = vet.ClinicId });
        }

        [Authorize]
        public async Task<IActionResult> Dislike(string id)
        {
            var vet = this.vetService.GetVetById(id);
            await this.vetService.UpdateDislikesAsync(id);
            return this.RedirectToAction(nameof(this.Index), "Vets", new { id = vet.ClinicId });
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
