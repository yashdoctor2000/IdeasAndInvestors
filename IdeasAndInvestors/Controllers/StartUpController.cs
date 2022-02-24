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
            HttpContext.Session.SetString("Pid", Convert.ToString(Pid));
            var rdFound = bkDb.PersonMasters.Where(usr => usr.Pid == Pid).FirstOrDefault();
            if (rdFound != null)
            {
                TempData["ErrMsg"] = "Welcome " + rdFound.Pname;
            }
            return View();
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
            TempData["FeedbackMsg"] = "Thankyou for your feedback!";
            return View();
        }
        

        
    }
}
