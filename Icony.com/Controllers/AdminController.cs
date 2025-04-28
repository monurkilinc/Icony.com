using Microsoft.AspNetCore.Mvc;

namespace Icony.Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
