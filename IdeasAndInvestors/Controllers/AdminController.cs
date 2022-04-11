using IdeasAndInvestors.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var startUpDetails = bkDb.PersonMasters.Where(usr => usr.Prollid == 2).ToList();
            return View(startUpDetails);
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
        public IActionResult AddCategory(CategoryMaster categoryMaster,IFormFile file)
        {
            string uniqueImageName = null;
            if (file != null)
            {
                string uploadimgfoldername = Path.Combine(henv.WebRootPath, "images\\CategoryImage\\");
                uniqueImageName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string finalPath = Path.Combine(uploadimgfoldername, uniqueImageName);
                file.CopyTo(new FileStream(finalPath, FileMode.Create));
                categoryMaster.Catimage = "images\\CategoryImage\\" + uniqueImageName;
            }
            
            bkDb.CategoryMasters.Add(categoryMaster);
            bkDb.SaveChanges();
            return RedirectToAction("AdminCategoryView");

        }
        [HttpGet]
        public IActionResult EditCategory(int Catid)
        {
            var id = Convert.ToString(Catid);
            var rdFound=bkDb.CategoryMasters.Where(usr=>usr.Catid==Catid).FirstOrDefault();
            if (rdFound != null)
            {
                return View(rdFound);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult EditCategory(CategoryMaster categorymaster,IFormFile file)
        {
            //var categoryMaster=new CategoryMaster();
            //var id = Convert.ToInt32(frm["Catid"]);
            //var new_name = Convert.ToString(frm["Catname"]);
            //var rdFound=bkDb.CategoryMasters.Where(usr=>usr.Catid==id).FirstOrDefault();
            //if (rdFound != null)
            //{
            //    rdFound.Catname = new_name;
            //    bkDb.SaveChanges();

            //}
            string uniqueImageName = null;
            if (file != null)
            {
                string uploadimgfoldername = Path.Combine(henv.WebRootPath, "images\\CategoryImage\\");
                uniqueImageName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string finalPath = Path.Combine(uploadimgfoldername, uniqueImageName);
                file.CopyTo(new FileStream(finalPath, FileMode.Create));
                categorymaster.Catimage = "images\\CategoryImage\\" + uniqueImageName;
                bkDb.Entry(categorymaster).State = EntityState.Modified;
                bkDb.SaveChanges();
                return RedirectToAction("AdminCategoryView");
            }
            else
            {
                bkDb.Entry(categorymaster).State = EntityState.Modified;
                bkDb.SaveChanges();
                return RedirectToAction("AdminCategoryView");
            }
        }
        public IActionResult DeleteCategory(int Catid)
        {
            var categoryMaster = new CategoryMaster();
            
            var rdFound = bkDb.CategoryMasters.Where(usr => usr.Catid == Catid).FirstOrDefault();
            if (rdFound != null)
            {
                bkDb.Entry(rdFound).State = EntityState.Deleted;
                bkDb.SaveChanges();
                return RedirectToAction("AdminCategoryView");
            }
            else
            {
                return RedirectToAction("AdminCategoryView");
            }
        }

        public IActionResult InvestmentDetails()
        {
            var rdFound = bkDb.InvestmentMasters.ToList();
            return View(rdFound);
        }
        public IActionResult FullyFledgedIdeas()
        {
            var investments = bkDb.InvestmentMasters.ToList();
            var ideas = bkDb.IdeaMasters.ToList();
            ViewBag.investments=investments;
            return View(ideas);
        }

        public IActionResult ContactInformation()
        {
            var contactInformation = bkDb.DonorMasters.ToList();
            return View(contactInformation);
        }

    }
}
