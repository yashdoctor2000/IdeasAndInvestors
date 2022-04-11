using IdeasAndInvestors.Models;
using Microsoft.AspNetCore.Mvc;

namespace IdeasAndInvestors.Controllers
{
    public class InvestorController : Controller
    {
        #region Default
        private readonly Models.IdeasAndInvestorsDbContext bkDb;
        private readonly IWebHostEnvironment henv;

        public InvestorController(Models.IdeasAndInvestorsDbContext bkDB, IWebHostEnvironment henv)
        {
            bkDb = bkDB;
            this.henv = henv;
        }
        #endregion Default
        public IActionResult InvestorHome(int Pid)
        {
            HttpContext.Session.SetString("Pid", Convert.ToString(Pid));
            var rdFound = bkDb.PersonMasters.Where(usr => usr.Pid == Pid).FirstOrDefault();
            if (rdFound != null)
            {
                ViewData["ErrMsg"] = "Welcome " + rdFound.Pname;
            }
            TempData["Pid"] = HttpContext.Session.GetString("Pid");
            return View();
        }
        [HttpGet]
        public IActionResult InvestorComplain()
        {
            var Pid= Convert.ToInt32(
                HttpContext.Session.GetString("Pid"));
            TempData["Pid"] = Pid;
            return View();
        }
        [HttpPost]
        public IActionResult InvestorComplain(IFormCollection frm)
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
        public IActionResult InvestorFeedback()
        {
            var Pid = Convert.ToInt32(
                HttpContext.Session.GetString("Pid"));
            TempData["Pid"] = Pid;
            return View();
        }
        [HttpPost]
        public IActionResult InvestorFeedback(IFormCollection frm)
        {
            FeedbackMaster feedbackMaster = new FeedbackMaster();
            feedbackMaster.Fdetails = Convert.ToString(frm["Fdetails"]);
            feedbackMaster.Experiencerate = Convert.ToString(frm["Experiencerate"]);
            feedbackMaster.Fdate = DateTime.Now;
            feedbackMaster.Pid = Convert.ToInt32(
                HttpContext.Session.GetString("Pid"));
            bkDb.FeedbackMasters.Add(feedbackMaster);
            bkDb.SaveChanges();
            TempData["FeedbackMsg"] = "Thankyou for your feedback!";
            TempData["Pid"]=feedbackMaster.Pid;
            return View();
            
        }

        public IActionResult InvestorExploreCategory()
        {
            var Pid = Convert.ToInt32(
                HttpContext.Session.GetString("Pid"));
            TempData["Pid"] = Pid;
            var qList = bkDb.CategoryMasters.ToList();
            return View(qList);
        }

        public IActionResult InvestorCategoryDetails(int Catid)
        {
            var Pid = Convert.ToInt32(
                HttpContext.Session.GetString("Pid"));
            TempData["Pid"] = Pid;
            var categoryDetails = bkDb.IdeaMasters.Where(usr => usr.Catid == Catid).ToList();
            var name=bkDb.CategoryMasters.Where(usr=>usr.Catid==Catid).FirstOrDefault();
            TempData["CategoryName"] = Convert.ToString(name.Catname);
            return View(categoryDetails);
        }

        public IActionResult InvestorIdeaView(int Iid)
        {
            var Pid = Convert.ToInt32(
                HttpContext.Session.GetString("Pid"));
            TempData["Pid"]= Pid;
            TempData["Found"] = null;
            ViewBag.backers=bkDb.InvestmentMasters.Where(usr=>usr.Iid==Iid).Count();
            var investment_done=bkDb.InvestmentMasters.Where(usr=>usr.Iid==Iid).ToList();
            var invested_amount = 0;
            foreach (var usr in investment_done){
                invested_amount = Convert.ToInt32(usr.Insamount)+invested_amount;
            }
            foreach (var usr in investment_done)
            {
                if (usr.Pid == Pid)
                {
                    TempData["Found"] = 1;
                }
            }
            var ideaDetails=bkDb.IdeaMasters.Where(usr=>usr.Iid==Iid).FirstOrDefault();
            ViewBag.remaining_amount = Convert.ToInt32(ideaDetails.IinvestmentNeeded) - invested_amount;
            ViewBag.invested_amount=invested_amount;
            var personID=Convert.ToInt32(ideaDetails.Pid);
            var duration = Convert.ToInt32(ideaDetails.IinvestmentDuration);
            DateTime registered_date = ideaDetails.Idate;
            DateTime due_date=registered_date.AddMonths(duration);
            TimeSpan difference = due_date- DateTime.Now;
            ViewBag.difference=difference.Days;
            ViewBag.due_date = due_date;
            ViewBag.personDetails=bkDb.PersonMasters.Where(usr=>usr.Pid==personID).FirstOrDefault();
            return View(ideaDetails);
        }
        [HttpGet]
        public IActionResult InvestorInvestmentView(int Iid)
        {
            var Pid = Convert.ToInt32(
                HttpContext.Session.GetString("Pid"));
            TempData["Pid"] = Pid;

            var rdFound = bkDb.IdeaMasters.Where(usr => usr.Iid == Iid).FirstOrDefault();
            var investmentMaster=bkDb.InvestmentMasters.Where(usr=>usr.Iid==Iid).ToList();
            var invested_amount = 0;
            foreach (var usr in investmentMaster)
            {
                invested_amount = invested_amount + Convert.ToInt32(usr.Insamount);
            }
            var remaining_amount = Convert.ToInt32(rdFound.IinvestmentNeeded) - invested_amount;
            ViewBag.remaining_amount=remaining_amount;
            ViewBag.invested_amount = invested_amount;
            ViewBag.Iid=Iid;
            ViewBag.Ititle = rdFound.Ititle;
            ViewBag.IRFLT10Pnt = rdFound.IRFLT10Pnt;
            ViewBag.IRFLT20Pnt= rdFound.IRFLT20Pnt;
            ViewBag.Idescription = rdFound.Idescription;
            ViewBag.IinvestmentNeeded = rdFound.IinvestmentNeeded;
            return View();
        }
        [HttpPost]
        public IActionResult InvestorInvestmentView(IFormCollection frm)
        {
            var investmentMaster=new InvestmentMaster();
            investmentMaster.Pid = Convert.ToInt32(HttpContext.Session.GetString("Pid"));
            var Pid=investmentMaster.Pid;
            investmentMaster.Insdate=DateTime.Now;
            investmentMaster.Instime = DateTime.Now;
            investmentMaster.Instype = "10%";
            investmentMaster.Insamount = Convert.ToInt32(frm["Insamount"]);
            var investment = Convert.ToInt32(TempData["IinvestmentNeeded"]);
            var percent = (investmentMaster.Insamount * 100) / investment;
            investmentMaster.Instype = Convert.ToString(percent);
            investmentMaster.Iid = Convert.ToInt32(frm["Iid"]);
            var amount = investmentMaster.Insamount;
            bkDb.InvestmentMasters.Add(investmentMaster);
            bkDb.SaveChanges();
            return RedirectToAction("InvestorPaymentInfo", new {Pid=Pid,Payment=amount});
        }
        public IActionResult InvestorPaymentInfo(int Pid,int Payment)
        {
            var amount=Payment;
            TempData["Pid"] = Pid;
            TempData["Insamount"]=amount;
            return View();
        }
        public IActionResult YourInvestments()
        {
            var Pid = Convert.ToInt32(
                HttpContext.Session.GetString("Pid"));
            TempData["Pid"] = Pid;
            var InvestList=bkDb.InvestmentMasters.Where(usr=>usr.Pid==Pid).ToList();
            var IdeaList = bkDb.IdeaMasters.ToList();
            ViewBag.IdeaList = IdeaList;
            return View(InvestList);
        }
        public IActionResult InvestorAboutUs()
        {
            var Pid = Convert.ToInt32(
                HttpContext.Session.GetString("Pid"));
            TempData["Pid"] = Pid;
            return View();
        }
        public IActionResult InvestorContactUs()
        {
            var Pid = Convert.ToInt32(
                HttpContext.Session.GetString("Pid"));
            TempData["Pid"] = Pid;
            return View();
        }
    }
}
