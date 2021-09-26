namespace PetSanctuary.Web.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using PetSanctuary.Common;
    using PetSanctuary.Services.Data.Blogs;
    using PetSanctuary.Services.Data.Counts;
    using PetSanctuary.Web.ViewModels.Blogs;

    using static PetSanctuary.Common.MessageConstants.Blog;

    public class BlogsController : BaseController
    {
        private readonly IBlogService blogService;
        private readonly ICountService countService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public BlogsController(
            IBlogService blogService,
            ICountService countService,
            IWebHostEnvironment webHostEnvironment)
        {
            this.blogService = blogService;
            this.countService = countService;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var model = this.blogService.GetAllBlogs();
            return this.View(model);
        }

        public IActionResult Recent()
        {
            var model = this.blogService.GetAllBlogs()
                .OrderByDescending(blog => blog.CreatedOn)
                .Take(3)
                .ToList();
            return this.View(model);
        }

        public IActionResult All([FromQuery] BlogQueryModel query)
        {
            query.Blogs = this.blogService
                 .GetAllBlogs(query.CurrentPage, query.ElementsPerPage)
                 .ToList();

            query.TotalPosts = this.countService.GetAllBlogsCount();

            return this.View(query);
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

            await this.blogService.CreateAsync(model.Title, model.Image, model.Description, userId, this.webHostEnvironment.WebRootPath);
            this.TempData["message"] = SuccessfullyCreated;
            return this.Redirect($"/Blogs");
        }

        [Authorize]
        public IActionResult Edit(string id)
        {
            var blog = this.blogService.GetBlogById(id);
            var model = new BlogEditFormModel
            {
                Title = blog.Title,
                Description = blog.Description,
            };
            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(string id, BlogEditFormModel model)
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

            await this.blogService.EditByIdAsync(id, model.Title, model.Image, model.Description, this.webHostEnvironment.WebRootPath);
            this.TempData["message"] = SuccessfullyEdited;
            return this.RedirectToAction("Blogs", "MyProfile");
        }

        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            await this.blogService.DeleteByIdAsync(id, this.webHostEnvironment.WebRootPath);
            this.TempData["message"] = SuccessfullyDeleted;
            return this.RedirectToAction("Blogs", "MyProfile");
        }
    }
}
