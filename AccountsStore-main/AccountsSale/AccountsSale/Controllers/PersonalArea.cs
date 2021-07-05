using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsSale.Controllers
{
    public class PersonalArea : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult PersonalData()
        {
            return View();
        }
    }
}
