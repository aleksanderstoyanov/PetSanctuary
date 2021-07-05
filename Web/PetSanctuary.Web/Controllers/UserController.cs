using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSanctuary.Web.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Profile()
        {
            return this.View();
        }
        public IActionResult Posts()
        {
            return this.View();
        }
        
    }
}
