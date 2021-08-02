namespace PetSanctuary.Web.Controllers
{
    using System.IO;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using PetSanctuary.Services.Data.Blogs;
    using PetSanctuary.Web.ViewModels.Blogs;

    public class BlogsController : BaseController
    {
        private readonly IBlogService blogService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public BlogsController(IBlogService blogService, IWebHostEnvironment webHostEnvironment)
        {
            this.blogService = blogService;
            this.webHostEnvironment = webHostEnvironment;
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

            var rootPath = this.webHostEnvironment.WebRootPath;
            await this.blogService.CreateAsync(model.Title, model.Image, model.Description, userId, rootPath);

            return this.Redirect($"/Blogs");
        }
    }
}
