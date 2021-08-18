namespace PetSanctuary.Web.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PetSanctuary.Common;
    using PetSanctuary.Data.Models;
    using PetSanctuary.Services.Data.Catalogs;
    using PetSanctuary.Services.Data.Counts;
    using PetSanctuary.Services.Data.EmailSender;
    using PetSanctuary.Web.ViewModels.Catalog;

    using static PetSanctuary.Common.MessageConstants.Catalog;

    public class CatalogController : BaseController
    {
        private readonly ICatalogService catalogService;
        private readonly ICountService countService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailSenderService emailSender;

        public CatalogController(
            ICatalogService catalogService,
            ICountService countService,
            UserManager<ApplicationUser> userManager,
            IEmailSenderService emailSender)
        {
            this.catalogService = catalogService;
            this.countService = countService;
            this.userManager = userManager;
            this.emailSender = emailSender;
        }

        public IActionResult Index([FromQuery] CatalogQueryModel query)
        {
            if (query.Type != "Dog" && query.Type != "Cat" && query.Type != "Other")
            {
                this.ModelState.AddModelError(nameof(query.Type), "Model is invalid !");
            }

            if (!this.ModelState.IsValid)
            {
                query.Type = "Dog";

                query.Pets = this.catalogService
                .GetPetsByType(query.CurrentPage, query.ElementsPerPage, query.Type)
                .Select(x => new CatalogViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    CreatedOn = x.CreatedOn,
                }).ToList();

                query.TotalPosts = this.countService.GetTotalPetsByType(query.Type);

                return this.View(query);
            }

            query.Pets = this.catalogService
                .GetPetsByType(query.CurrentPage, query.ElementsPerPage, query.Type)
                .Select(x => new CatalogViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    CreatedOn = x.CreatedOn,
                }).ToList();

            query.TotalPosts = this.countService.GetTotalPetsByType(query.Type);

            return this.View(query);
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CatalogFormCreateViewModel model)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (model.Type != "Dog" && model.Type != "Cat" && model.Type != "Other")
            {
                this.ModelState.AddModelError(nameof(model.Type), "Pet type is invalid");
            }

            if (model.Gender != "Male" && model.Gender != "Female")
            {
                this.ModelState.AddModelError(nameof(model.Gender), "Pet gender is invalid");
            }

            if (model.IsVaccinated != "Yes" && model.IsVaccinated != "No")
            {
                this.ModelState.AddModelError(nameof(model.IsVaccinated), "Pet's vaccination is invalid");
            }

            if (model.Image != null)
            {
                var extension = Path.GetExtension(model.Image.FileName);
                if (extension != ".jpeg" && extension != ".jpg" && extension != ".gif" && extension != ".png")
                {
                    this.ModelState.AddModelError(nameof(model.Image), "Allowed file extensions are jpeg, jpg, gif and png");
                }
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.catalogService.Create(model.Name, model.Age, model.Image, model.Type, model.Gender, model.City, model.Address, model.IsVaccinated, userId, GlobalConstants.WwwRootPath);
            this.TempData["message"] = SuccessfullyCreated;
            return this.RedirectToAction(nameof(this.Index), "Catalog");
        }

        [Authorize]
        public IActionResult Edit(string id)
        {
            var pet = this.catalogService.GetPetById(id);
            var model = new CatalogEditFormModel
            {
                Name = pet.Name,
                Age = pet.Age,
                Address = pet.Address,
                City = pet.City,
                Gender = pet.Gender.ToString(),
                Type = pet.Type.ToString(),
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(string id, CatalogEditFormModel model)
        {
            if (model.Type != "Dog" && model.Type != "Cat" && model.Type != "Other")
            {
                this.ModelState.AddModelError(nameof(model.Type), "Pet type is invalid");
            }

            if (model.Gender != "Male" && model.Gender != "Female")
            {
                this.ModelState.AddModelError(nameof(model.Gender), "Pet gender is invalid");
            }

            if (model.Image != null)
            {
                var extension = Path.GetExtension(model.Image.FileName);
                if (extension != ".jpeg" && extension != ".jpg" && extension != ".gif" && extension != ".png")
                {
                    this.ModelState.AddModelError(nameof(model.Image), "Allowed file extensions are jpeg, jpg, gif and png");
                }
            }

            if (model.IsVaccinated != "Yes" && model.IsVaccinated != "No")
            {
                this.ModelState.AddModelError(nameof(model.IsVaccinated), "Pet's vaccination type is invalid");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.catalogService.EditPetById(id, model.Name, model.Age, model.Image, model.Type, model.Gender, model.IsVaccinated, model.City, model.Address, GlobalConstants.WwwRootPath);
            if (this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.RedirectToAction(nameof(this.Index), "Catalog");
            }

            this.TempData["message"] = SuccessfullyEdited;

            return this.RedirectToAction("Posts", "MyProfile");
        }

        [Authorize]

        public async Task<IActionResult> Delete(string id)
        {
            await this.catalogService.DeletePetById(id, GlobalConstants.WwwRootPath);
            if (this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.RedirectToAction(nameof(this.Index), "Catalog");
            }

            this.TempData["message"] = SuccessfullyDeleted;
            return this.RedirectToAction("Posts", "MyProfile");
        }

        public IActionResult Details(string id)
        {
            var model = this.catalogService.GetPetById(id);
            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Details(string id, string subject = null)
        {
            var pet = this.catalogService.GetPetById(id);
            var owner = await this.userManager.FindByIdAsync(pet.OwnerId);
            var user = this.User.Identity.Name;
            await this.emailSender.SendEmailAsync(user, owner.UserName, $"Saving pet",
                $@"Hello,I am interested in aquiring the pet you have listed named Name:{pet.Name} Type:{pet.Type} Address:{pet.City}-{pet.Address}");
            return this.RedirectToAction(nameof(this.Index), "Catalog");
        }
    }
}
