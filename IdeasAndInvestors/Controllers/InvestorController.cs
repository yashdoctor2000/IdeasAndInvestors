using Microsoft.AspNetCore.Mvc;

namespace IdeasAndInvestors.Controllers
{
    public class InvestorController : Controller
    {
        public IActionResult InvestorHome()
        {
            return View();
        }
    }
}
