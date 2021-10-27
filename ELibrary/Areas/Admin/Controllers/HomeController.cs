﻿using Microsoft.AspNetCore.Mvc;

namespace ELibrary.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
