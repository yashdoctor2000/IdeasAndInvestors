using IdeasAndInvestors.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace IdeasAndInvestors.Controllers
{
    public class AdminController : Controller
    {
        #region Default
        private readonly IConfiguration _configuration;
        private readonly Models.IdeasAndInvestorsDbContext bkDb;
        private readonly IWebHostEnvironment henv;

        public AdminController(IConfiguration configuration,Models.IdeasAndInvestorsDbContext bkDB, IWebHostEnvironment henv)
        {
            _configuration = configuration;
            bkDb = bkDB;
            this.henv = henv;
        }
        #endregion Default
        public IActionResult AdminHome()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Pid")))
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
        public IActionResult AdminViewInvestorDetails()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Pid")))
            {
                return RedirectToAction("Login", "Login");
            }
            var investorDetails = bkDb.PersonMasters.Where(usr => usr.Prollid == 3).ToList();
            return View(investorDetails);
        }
        public IActionResult AdminViewStartUpDetails()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Pid")))
            {
                return RedirectToAction("Login", "Login");
            }
            var startUpDetails = bkDb.PersonMasters.Where(usr => usr.Prollid == 2).ToList();
            return View(startUpDetails);
        }
        public IActionResult AdminComplainDetails()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Pid")))
            {
                return RedirectToAction("Login", "Login");
            }
            var complainDetails = bkDb.ComplainMasters.ToList();
            var personDetails = bkDb.PersonMasters.ToList();
            ViewBag.perDetails = personDetails;
            return View(complainDetails);
        }
        public IActionResult AdminFeedbackDetails()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Pid")))
            {
                return RedirectToAction("Login", "Login");
            }
            var feedbackDetails = bkDb.FeedbackMasters.ToList();
            var personDetails=bkDb.PersonMasters.ToList();
            ViewBag.perDetails = personDetails;
            return View(feedbackDetails);
        }
        public IActionResult AdminCategoryView()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Pid")))
            {
                return RedirectToAction("Login", "Login");
            }
            var categoryDetails=bkDb.CategoryMasters.ToList();
            return View(categoryDetails);
            
        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Pid")))
            {
                return RedirectToAction("Login", "Login");
            }
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
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Pid")))
            {
                return RedirectToAction("Login", "Login");
            }
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
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Pid")))
            {
                return RedirectToAction("Login", "Login");
            }
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
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Pid")))
            {
                return RedirectToAction("Login", "Login");
            }
            List<InvestmentViewModel> investments = new List<InvestmentViewModel>();

            string connectionString = _configuration.GetConnectionString("IdeasAndInvestorsDBConnection");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetInvestmentDetails", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            InvestmentViewModel investment = new InvestmentViewModel
                            {
                                // Assuming these are the names of the columns returned by the stored procedure
                                Insid = (int)reader["Insid"],
                                Pid = (int)reader["Pid"],
                                Pname = reader["Pname"].ToString(),
                                Insamount = (int)reader["Insamount"],
                                Insdate = (DateTime)reader["Insdate"]
                            };
                            investments.Add(investment);
                        }
                    }
                }
            }

            return View(investments);
        }
        public IActionResult FullyFledgedIdeas()
        {
            //var investments = bkDb.InvestmentMasters.ToList();
            //var ideas = bkDb.IdeaMasters.ToList();
            //ViewBag.investments=investments;
            //return View(ideas);
            List<InvestmentCompletedModel> investmentscompleted = new List<InvestmentCompletedModel>();

            string connectionString = _configuration.GetConnectionString("IdeasAndInvestorsDBConnection");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InvestmentCompletedDetails", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            InvestmentCompletedModel investmentcompleted = new InvestmentCompletedModel
                            {
                                // Assuming these are the names of the columns returned by the stored procedure
                            
                                Pid = (int)reader["Pid"],
                                Pname = reader["Pname"].ToString(),
                                Ititle = reader["Ititle"].ToString(),
                                Idescription = reader["Idescription"].ToString(),
                                Iinvestmentneeded = reader["Iinvestmentneeded"].ToString(),
                                Iimage = reader["Iimage"].ToString()
                            };
                            investmentscompleted.Add(investmentcompleted);
                        }
                    }
                }
            }

            return View(investmentscompleted);


        }

        public IActionResult ContactInformation()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Pid")))
            {
                return RedirectToAction("Login", "Login");
            }
            var contactInformation = bkDb.DonorMasters.ToList();
            return View(contactInformation);
        }

    }
}
