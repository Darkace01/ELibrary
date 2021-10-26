using Microsoft.AspNetCore.Mvc;

namespace ELibrary.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
