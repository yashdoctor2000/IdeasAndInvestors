using IdeasAndInvestors.Models;
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
        public IActionResult AdminComplainDetails()
        {
            var complainDetails = bkDb.ComplainMasters.ToList();
            var personDetails = bkDb.PersonMasters.ToList();
            ViewBag.perDetails = personDetails;
            return View(complainDetails);
        }
        public IActionResult AdminFeedbackDetails()
        {
            var feedbackDetails = bkDb.FeedbackMasters.ToList();
            var personDetails=bkDb.PersonMasters.ToList();
            ViewBag.perDetails = personDetails;
            return View(feedbackDetails);
        }
        public IActionResult AdminCategoryView()
        {
            var categoryDetails=bkDb.CategoryMasters.ToList();
            return View(categoryDetails);
            
        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(IFormCollection frm)
        {
            CategoryMaster categoryMaster = new CategoryMaster();
            categoryMaster.Catname= Convert.ToString(frm["Catname"]);
            bkDb.CategoryMasters.Add(categoryMaster);
            bkDb.SaveChanges();
            return View();
            
        }
        public IActionResult EditCategory()
        {
            //var id = Convert.ToString(CatId);
            //var rdFound=bkDb.CategoryMasters.Where(usr=>usr.Catname==CatId).FirstOrDefault();
            //if (rdFound != null)
            //{
            //    TempData["Catid"] = rdFound.Catid;
            //}
            return View();
        }


    }
}
