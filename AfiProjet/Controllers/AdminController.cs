using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AfiProjet.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Roles = @"vs2017eric\Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}