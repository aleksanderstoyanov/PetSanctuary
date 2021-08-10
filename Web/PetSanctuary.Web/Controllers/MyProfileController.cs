namespace PetSanctuary.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
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

        public MyProfileController(
            ICatalogService catalogService,
            IBlogService blogService,
            ICountService countService,
            UserManager<ApplicationUser> userManager)
        {
            this.catalogService = catalogService;
            this.blogService = blogService;
            this.countService = countService;
            this.userManager = userManager;
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
    }
}
