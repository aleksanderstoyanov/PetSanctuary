namespace PetSanctuary.Web.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PetSanctuary.Common;
    using PetSanctuary.Data.Models;
    using PetSanctuary.Services.Data.Blogs;
    using PetSanctuary.Services.Data.Catalogs;
    using PetSanctuary.Services.Data.Counts;
    using PetSanctuary.Web.ViewModels.User;

    public class MyProfileController : BaseController
    {
        private readonly ICatalogService catalogService;
        private readonly IBlogService blogService;
        private readonly ICountService countService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MyProfileController(
            ICatalogService catalogService,
            IBlogService blogService,
            ICountService countService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment webHostEnvironment)
        {
            this.catalogService = catalogService;
            this.blogService = blogService;
            this.countService = countService;
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userPhoneNumber = await this.userManager.GetPhoneNumberAsync(user);

            var model = new ProfileViewModel
            {
                Email = this.User.Identity.Name,
                NumberOfPosts = this.countService.GetUserPostsCount(user.Id, this.User.IsInRole(GlobalConstants.AdministratorRoleName)),
                PhoneNumber = userPhoneNumber,
                NumberOfBlogs = this.countService.GetUserBlogsCount(user.Id, this.User.IsInRole(GlobalConstants.AdministratorRoleName)),
            };
            return this.View(model);
        }

        [Authorize]
        public IActionResult Posts([FromQuery] PetPostQueryModel query)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool isAdmin = this.User.IsInRole(GlobalConstants.AdministratorRoleName);

            query.Pets = this.catalogService
               .GetAllUserPets(userId, query.CurrentPage, query.ElementsPerPage, isAdmin)
               .ToList();

            query.TotalPosts = this.countService.GetUserPostsCount(userId, isAdmin);

            return this.View(query);
        }

        [Authorize]
        public IActionResult EditPost(string id)
        {
            var pet = this.catalogService.GetPetById(id);
            var model = new PetPostViewModel
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
        public async Task<IActionResult> EditPost(string id, PetPostViewModel model)
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

            var rootPath = this.webHostEnvironment.WebRootPath;
            await this.catalogService.EditPetById(id, model.Name, model.Age, model.Image, model.Type, model.Gender, model.IsVaccinated, model.City, model.Address, rootPath);
            return this.RedirectToAction(nameof(this.Posts), "MyProfile");
        }

        [Authorize]

        public async Task<IActionResult> DeletePost(string id)
        {
            var rootPath = this.webHostEnvironment.WebRootPath;
            await this.catalogService.DeletePetById(id, rootPath);
            return this.RedirectToAction(nameof(this.Posts), "MyProfile");
        }

        [Authorize]
        public IActionResult Blogs([FromQuery] BlogPostQueryModel query)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool isAdmin = this.User.IsInRole(GlobalConstants.AdministratorRoleName);

            var blogs = this.blogService
              .GetAllUserBlogs(userId, query.CurrentPage, query.ElementsPerPage, isAdmin)
              .ToList();

            query.Blogs = blogs;
            query.TotalPosts = this.countService.GetUserBlogsCount(userId, isAdmin);

            return this.View(query);
        }


        [Authorize]
        public IActionResult EditBlog(string id)
        {
            var blog = this.blogService.GetBlogById(id);
            var model = new BlogPostViewModel
            {
                Title = blog.Title,
                Description = blog.Description,
            };
            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditBlog(string id, BlogPostViewModel model)
        {
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

            string rootPath = this.webHostEnvironment.WebRootPath;
            await this.blogService.EditByIdAsync(id, model.Title, model.Image, model.Description, rootPath);
            return this.RedirectToAction(nameof(this.Blogs), "MyProfile");
        }

        [Authorize]
        public async Task<IActionResult> DeleteBlog(string id)
        {
            var rootPath = this.webHostEnvironment.WebRootPath;
            await this.blogService.DeleteByIdAsync(id, rootPath);
            return this.RedirectToAction(nameof(this.Blogs), "MyProfile");
        }
    }
}
