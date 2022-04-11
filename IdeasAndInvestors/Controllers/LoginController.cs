using IdeasAndInvestors.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdeasAndInvestors.Controllers
{
    public class LoginController : Controller
    {
        #region Default
        private readonly Models.IdeasAndInvestorsDbContext bkDb;
        private readonly IWebHostEnvironment henv;

        public LoginController(Models.IdeasAndInvestorsDbContext bkDB, IWebHostEnvironment henv)
        {
            bkDb = bkDB;
            this.henv = henv;
        }
        #endregion Default
        public IActionResult Home()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {            
            return View();
        }

        [HttpPost]
        public IActionResult Login(IFormCollection frm)
        {
            var email = Convert.ToString(frm["Email"]);
            var password = Convert.ToString(frm["Password"]);
            var rdFound = bkDb.PersonMasters.Where(usr => usr.Pemail == email && usr.Ppassword == password).FirstOrDefault();
            if (rdFound != null)
            {
                if (rdFound.Prollid == 2)
                {
                    return RedirectToAction("StartUpHome", "StartUp", new { rdFound.Pid });
                }
                else if (rdFound.Prollid == 3)
                {
                    return RedirectToAction("InvestorHome", "Investor", new { rdFound.Pid });
                }
                else if (rdFound.Prollid == 1)
                {
                    return RedirectToAction("AdminHome", "Admin");
                }
            }

            
            
                TempData["ErrMsg"] = "Invalid Email or Password";
                return View();
            

           
        }
        [HttpGet]
        public IActionResult SignUPStartUp()
        {
            return View();

        }
        [HttpPost]
        public IActionResult SignUPStartUp(PersonMaster personMaster, IFormFile file)
        {

            string uniqueImageName = null;
            if (file != null)
            {
                string uploadimgfoldername = Path.Combine(henv.WebRootPath, "images\\StartupImage");
                uniqueImageName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string finalPath = Path.Combine(uploadimgfoldername, uniqueImageName);
                file.CopyTo(new FileStream(finalPath, FileMode.Create));
                personMaster.Pimage = "images\\StartupImage\\" + uniqueImageName;
            }
            
            personMaster.Pqid = 0;
            personMaster.Panswer = "NoAnswer";
            personMaster.Prollid = 2;//2 for startup
            bkDb.PersonMasters.Add(personMaster);
            bkDb.SaveChanges();
            return RedirectToAction("Login");

        }

        [HttpGet]
        public IActionResult SignUPInvestor()
        {
            var qList = bkDb.QuestionMasters.ToList();
            return View(qList);
        }
        [HttpPost]
        public IActionResult SignUPInvestor(PersonMaster personMaster,IFormFile file)
        {
            string uniqueImageName = null;
            if (file != null)
            {
                string uploadimgfoldername = Path.Combine(henv.WebRootPath, "images\\StartupImage");
                uniqueImageName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string finalPath = Path.Combine(uploadimgfoldername, uniqueImageName);
                file.CopyTo(new FileStream(finalPath, FileMode.Create));
                personMaster.Pimage = "images\\StartupImage\\" + uniqueImageName;
            }
            personMaster.Pqualification = "NoAnswer";
            personMaster.Prollid = 3;//3 for investors
            bkDb.PersonMasters.Add(personMaster);
            bkDb.SaveChanges();
            return RedirectToAction("Login");
            
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(IFormCollection frm)
        {
            var email = Convert.ToString(frm["Email"]);
            var password = Convert.ToString(frm["Password"]);
            var rdFound = bkDb.PersonMasters.Where(usr => usr.Pemail == email && usr.Ppassword == password).FirstOrDefault();
            if (rdFound != null)
            {
                rdFound.Ppassword= Convert.ToString(frm["CPassword"]);
                bkDb.Entry(rdFound).State = EntityState.Modified;
                bkDb.SaveChanges();
                TempData["ErrMsg"] = "Password Updated Successfully";
            }
            else
            {
                TempData["ErrMsg"] = "Invalid Password or Email";
            }
            return View();
        }
        public JsonResult CheckEmail(string Email)
        {
            var chkEmail=bkDb.PersonMasters.Where(q=>q.Pemail == Email).Count();
            if (chkEmail > 0)
            {
                return Json(false);
            }
            else
            {
                return Json(true);
            }
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ContactUs(DonorMaster donorMaster)
        {
            bkDb.DonorMasters.Add(donorMaster);
            bkDb.SaveChanges();
            return View();
        }
        
    }
}
