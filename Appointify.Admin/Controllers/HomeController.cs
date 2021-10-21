using Microsoft.AspNetCore.Mvc;

using Appointify.Admin.Utilities;

namespace Appointify.Admin.Controllers
{
    [Authentication]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error(int id)
        {
            return View(id);
        }
    }
}
