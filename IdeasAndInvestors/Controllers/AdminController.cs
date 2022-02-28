using Microsoft.AspNetCore.Mvc;

namespace IdeasAndInvestors.Controllers
{
    public class AdminController : Controller
    {
        #region Default
        private readonly Models.IdeasAndInvestorsDbContext bkDb;
        private readonly IWebHostEnvironment henv;

        public AdminController(Models.IdeasAndInvestorsDbContext bkDB, IWebHostEnvironment henv)
        {
            bkDb = bkDB;
            this.henv = henv;
        }
        #endregion Default
        public IActionResult AdminHome()
        {
            return View();
        }
        public IActionResult AdminViewInvestorDetails()
        {
            var investorDetails = bkDb.PersonMasters.Where(usr => usr.Prollid == 3).ToList();
            return View(investorDetails);
        }
        public IActionResult AdminViewStartUpDetails()
        {
            var investorDetails = bkDb.PersonMasters.Where(usr => usr.Prollid == 2).ToList();
            return View(investorDetails);
        }
    }
}
