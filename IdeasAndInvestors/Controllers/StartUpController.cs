using IdeasAndInvestors.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdeasAndInvestors.Controllers
{
    public class StartUpController : Controller
    {
        #region Default
        private readonly Models.IdeasAndInvestorsDbContext bkDb;
        private readonly IWebHostEnvironment henv;

        public StartUpController(Models.IdeasAndInvestorsDbContext bkDB, IWebHostEnvironment henv)
        {
            bkDb = bkDB;
            this.henv = henv;
        }
        #endregion Default
        public IActionResult StartUpHome(int Pid)
        {
            var startUpDetails = bkDb.IdeaMasters.Where(usr => usr.Pid == Pid).ToList();
            HttpContext.Session.SetString("Pid", Convert.ToString(Pid));
            TempData["Pid"] = Convert.ToInt32(HttpContext.Session.GetString("Pid"));
            var rdFound = bkDb.PersonMasters.Where(usr => usr.Pid == Pid).FirstOrDefault();
            var investmentMaster = bkDb.InvestmentMasters.ToList();
            ViewBag.investmentMaster = investmentMaster;
            Int32 length = startUpDetails.Count;
            TempData["Found"] = null;
            if (length != 0) 
            {
                TempData["Found"] = 1;
            }
            if (rdFound != null)
            {
                TempData["ErrMsg"] = "Welcome " + rdFound.Pname;
            }
            return View(startUpDetails);
        }
        [HttpGet]
        public IActionResult StartUpComplain()
        {
            TempData["Pid"] = Convert.ToInt32(HttpContext.Session.GetString("Pid"));
            return View();
        }
        [HttpPost]
        public IActionResult StartUpComplain(IFormCollection frm)
        {
            ComplainMaster complainMaster = new ComplainMaster();
            complainMaster.Cdetails = Convert.ToString(frm["Cdetails"]);
            complainMaster.Pid = Convert.ToInt32(frm["Pid"]);
            //complainMaster.Pid = Convert.ToInt32(
                //HttpContext.Session.GetString("Pid"));
            bkDb.ComplainMasters.Add(complainMaster);
            TempData["Pid"] = complainMaster.Pid;
            bkDb.SaveChanges();
            TempData["ComplainMsg"] = "Your complain is submitted successfully!";
            return View();
        }
        [HttpGet]
        public IActionResult StartUpFeedback()
        {
            TempData["Pid"] = Convert.ToInt32(HttpContext.Session.GetString("Pid"));
            return View();
        } 
        [HttpPost]
        public IActionResult StartUpFeedback(IFormCollection frm)
        {
            FeedbackMaster feedbackMaster = new FeedbackMaster();
            feedbackMaster.Fdetails = Convert.ToString(frm["Fdetails"]);
            feedbackMaster.Experiencerate = Convert.ToString(frm["Experiencerate"]);
            feedbackMaster.Fdate=DateTime.Now;
            feedbackMaster.Pid = Convert.ToInt32(frm["Pid"]);
            //feedbackMaster.Pid = Convert.ToInt32(
                //HttpContext.Session.GetString("Pid"));
            bkDb.FeedbackMasters.Add(feedbackMaster);
            TempData["Pid"] = feedbackMaster.Pid;
            bkDb.SaveChanges();
            TempData["FeedbackMsg"] = "Thankyou for your feedback!";
            return View();
        }
        [HttpGet]
        public IActionResult StartUpAddIdea()
        {
            TempData["Pid"] = Convert.ToInt32(HttpContext.Session.GetString("Pid"));
            var qList = bkDb.CategoryMasters.ToList();
            return View(qList);
        }
        [HttpPost]
        public IActionResult StartUpAddIdea(IdeaMaster ideaMaster,IFormFile file)
        {
            string uniqueImageName = null;
            if (file != null)
            {
                string uploadimgfoldername = Path.Combine(henv.WebRootPath, "images\\StartupImage\\StartUpIdeaPhoto");
                uniqueImageName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string finalPath = Path.Combine(uploadimgfoldername, uniqueImageName);
                file.CopyTo(new FileStream(finalPath, FileMode.Create));
                ideaMaster.Iimage = "images\\StartupImage\\StartUpIdeaPhoto\\" + uniqueImageName;
            }
            string str = ideaMaster.Ividurl;
            string new_str = str.Replace("watch?v=", "embed/");
            //ideaMaster.Pid = Convert.ToInt32(
              //HttpContext.Session.GetString("Pid"));
            //ideaMaster.Pid = Convert.ToInt32(TempData["Pid"]);
            ideaMaster.Idate=DateTime.Now;
            ideaMaster.Ividurl = new_str;
            
            bkDb.IdeaMasters.Add(ideaMaster);
            TempData["Pid"] = ideaMaster.Pid;
            bkDb.SaveChanges();
            return RedirectToAction("StartUpHome", new {Pid=ideaMaster.Pid});
        }
        public IActionResult DeleteIdea(int Iid)
        {
            var ideaMaster =new  IdeaMaster();
            var rdFound=bkDb.IdeaMasters.Where(usr=>usr.Iid == Iid).FirstOrDefault();
            if (rdFound != null)
            {
                bkDb.Entry(rdFound).State = EntityState.Deleted;
                bkDb.SaveChanges();
                return RedirectToAction("StartUpHome", new { Pid=rdFound.Pid });
            }
            else
            {
                return RedirectToAction("StartUpHome");
            }
        }
        [HttpGet]
        public IActionResult StartUpEditIdea(int Iid)
        {
            var ideaMaster = new IdeaMaster();
            var rdFound=bkDb.IdeaMasters.Where(usr=> usr.Iid==Iid).FirstOrDefault();
            var qList = bkDb.CategoryMasters.ToList();
            if (rdFound != null)
            {
                ViewBag.qList = qList;
                return View(rdFound);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult StartUpEditIdea(IdeaMaster ideaMaster, IFormFile file)
        {
            string uniqueImageName = null;
            if (ideaMaster.Ividurl.Contains("watch?v="))
            {
                string str = ideaMaster.Ividurl;
                string new_str = str.Replace("watch?v=", "embed/");
                ideaMaster.Ividurl = new_str;


                if (file != null)
                {
                    string uploadimgfoldername = Path.Combine(henv.WebRootPath, "images\\StartupImage\\StartUpIdeaPhoto");
                    uniqueImageName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string finalPath = Path.Combine(uploadimgfoldername, uniqueImageName);
                    file.CopyTo(new FileStream(finalPath, FileMode.Create));
                    ideaMaster.Iimage = "images\\StartupImage\\StartUpIdeaPhoto\\" + uniqueImageName;
                    bkDb.Entry(ideaMaster).State = EntityState.Modified;
                    TempData["Pid"] = ideaMaster.Pid;
                    bkDb.SaveChanges();
                    return RedirectToAction("StartUpHome", new { Pid = ideaMaster.Pid });
                }
                else
                {
                    bkDb.Entry(ideaMaster).State = EntityState.Modified;
                    TempData["Pid"] = ideaMaster.Pid;
                    bkDb.SaveChanges();
                    return RedirectToAction("StartUpHome", new { Pid = ideaMaster.Pid });
                }
            }
            else
            {
                if (file != null)
                {
                    string uploadimgfoldername = Path.Combine(henv.WebRootPath, "images\\StartupImage\\StartUpIdeaPhoto");
                    uniqueImageName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string finalPath = Path.Combine(uploadimgfoldername, uniqueImageName);
                    file.CopyTo(new FileStream(finalPath, FileMode.Create));
                    ideaMaster.Iimage = "images\\StartupImage\\StartUpIdeaPhoto\\" + uniqueImageName;
                    bkDb.Entry(ideaMaster).State = EntityState.Modified;
                    TempData["Pid"] = ideaMaster.Pid;
                    bkDb.SaveChanges();
                    return RedirectToAction("StartUpHome", new { Pid = ideaMaster.Pid });
                }
                else
                {
                    bkDb.Entry(ideaMaster).State = EntityState.Modified;
                    TempData["Pid"] = ideaMaster.Pid;
                    bkDb.SaveChanges();
                    return RedirectToAction("StartUpHome", new { Pid = ideaMaster.Pid });
                }
            }
        }

        public IActionResult StartUpYourInvestors()
        {            
            var Pid = Convert.ToInt32(HttpContext.Session.GetString("Pid"));
            TempData["Pid"] = Pid;
            var investors=bkDb.InvestmentMasters.ToList();
            var ideas=bkDb.IdeaMasters.Where(usr=>usr.Pid==Pid).ToList();
            var personMaster = bkDb.PersonMasters.ToList();
            ViewBag.personMaster=personMaster;
            ViewBag.Investors = investors;
            return View(ideas);
        }
        public IActionResult StartUpAboutUs()
        {
            var Pid = Convert.ToInt32(
                HttpContext.Session.GetString("Pid"));
            TempData["Pid"] = Pid;
            return View();
        }
        public IActionResult StartUpContactUs()
        {
            var Pid = Convert.ToInt32(
                HttpContext.Session.GetString("Pid"));
            TempData["Pid"] = Pid;
            return View();
        }
    }
}
