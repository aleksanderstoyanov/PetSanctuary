namespace PetSanctuary.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
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
               .Select(vet => new VetsByIdViewModel
               {
                   Id = vet.Id,
                   FirstName = vet.FirstName,
                   Surname = vet.Surname,
                   Likes = vet.Likes,
                   Dislikes = vet.Dislikes,
               }).ToList();
            return this.View(model);
        }

        [Authorize]
        public async Task<IActionResult> Like(string id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var vet = this.vetService.GetVetById(id);
            await this.vetService.UpdateLikesAsync(id, userId);
            return this.RedirectToAction(nameof(this.Index), "Vets", new { id = vet.ClinicId });
        }

        [Authorize]
        public async Task<IActionResult> Dislike(string id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var vet = this.vetService.GetVetById(id);
            await this.vetService.UpdateDislikesAsync(id, userId);
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
