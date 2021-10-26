using Microsoft.AspNetCore.Mvc;

namespace ELibrary.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult SignIn()
        {
            return View();
        }
    }
}
