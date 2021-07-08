using Microsoft.AspNetCore.Mvc;
using PetSanctuary.Web.ViewModels.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSanctuary.Web.Controllers
{
    public class BlogsController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
        public IActionResult Comments()
        {
            return this.View();
        }
        public IActionResult Create()
        {
            return this.View();
        }
        [HttpPost]
        public IActionResult Create(BlogFormCreateViewModel model)
        {
            return this.Redirect("/Blogs");
        }
        
    }
}
