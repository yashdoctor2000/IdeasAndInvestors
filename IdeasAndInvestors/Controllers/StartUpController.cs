using Microsoft.AspNetCore.Mvc;

namespace IdeasAndInvestors.Controllers
{
    public class StartUpController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }
    }
}
