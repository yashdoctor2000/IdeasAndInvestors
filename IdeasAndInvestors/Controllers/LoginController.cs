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
                TempData["ErrMsg"] = "Login Successfull";
            }
            else
            {
                TempData["ErrMsg"] = "Invalid Email or Password";
            }
            
            return View();
        }
        [HttpGet]
        public IActionResult SignUP()
        {
            return View();

        }
        [HttpPost]
        public IActionResult SignUP(IFormCollection frm)
        {
            return View();
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
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }
        
    }
}
