using IdeasAndInvestors.Models;
using Microsoft.AspNetCore.Mvc;

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
            if (rdFound != null)
            {
                TempData["ErrMsg"] = "Welcome " + rdFound.Pname;
            }
            return View(startUpDetails);
        }
        [HttpGet]
        public IActionResult StartUpComplain()
        {
            return View();
        }
        [HttpPost]
        public IActionResult StartUpComplain(IFormCollection frm)
        {
            ComplainMaster complainMaster = new ComplainMaster();
            complainMaster.Cdetails = Convert.ToString(frm["Cdetails"]);
            complainMaster.Pid = Convert.ToInt32(
                HttpContext.Session.GetString("Pid"));
            bkDb.ComplainMasters.Add(complainMaster);
            bkDb.SaveChanges();
            TempData["ComplainMsg"] = "Your complain is submitted successfully!";
            TempData["Pid"] = complainMaster.Pid;
            return View();
        }
        [HttpGet]
        public IActionResult StartUpFeedback()
        {
            return View();
        } 
        [HttpPost]
        public IActionResult StartUpFeedback(IFormCollection frm)
        {
            FeedbackMaster feedbackMaster = new FeedbackMaster();
            feedbackMaster.Fdetails = Convert.ToString(frm["Fdetails"]);
            feedbackMaster.Experiencerate = Convert.ToString(frm["Experiencerate"]);
            feedbackMaster.Fdate=DateTime.Now;
            feedbackMaster.Pid = Convert.ToInt32(
                HttpContext.Session.GetString("Pid"));
            bkDb.FeedbackMasters.Add(feedbackMaster);
            bkDb.SaveChanges();
            TempData["Pid"]=feedbackMaster.Pid;
            TempData["FeedbackMsg"] = "Thankyou for your feedback!";
            return View();
        }
        [HttpGet]
        public IActionResult StartUpAddIdea()
        {
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
            ideaMaster.Pid = Convert.ToInt32(
              HttpContext.Session.GetString("Pid"));
            //ideaMaster.Pid = Convert.ToInt32(TempData["Pid"]);
            ideaMaster.Idate=DateTime.Now;
            TempData["Pid"] = ideaMaster.Pid;
            bkDb.IdeaMasters.Add(ideaMaster);
            bkDb.SaveChanges();
            TempData["ComplainMsg"] = "Idea Added Successfully";
            return View();
        }
    }
}
