using Microsoft.AspNetCore.Mvc;

namespace IdeasAndInvestors.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult AdminHome()
        {
            return View();
        }
    }
}
