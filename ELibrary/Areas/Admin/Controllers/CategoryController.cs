using Microsoft.AspNetCore.Mvc;

namespace ELibrary.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin")]
    public class CategoryController : Controller
    {
        [Route("categories")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
