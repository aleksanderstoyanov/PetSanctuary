namespace PetSanctuary.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PetSanctuary.Services.Data.Blogs;
    using PetSanctuary.Web.ViewModels.Blogs;

    public class BlogsController : BaseController
    {
        private readonly IBlogService blogService;

        public BlogsController(IBlogService blogService)
        {
            this.blogService = blogService;
        }

        public IActionResult Index()
        {
            var model = this.blogService.GetAllBlogs();
            return this.View(model);
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(BlogFormCreateViewModel model)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.blogService.CreateAsync(model.Title, model.Image, model.Description, userId);

            return this.Redirect($"/Blogs");
        }

    }
}
