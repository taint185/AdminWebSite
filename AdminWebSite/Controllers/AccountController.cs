using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminWebSite.Models;
using WebSite.Models;


namespace WebSite.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        //private ICustomerRepository iCustomerRepository = new CustomerRepository();
        //[HttpGet]
        //public ActionResult MyAccount()
        //{
        //    return View("MyAccount");
        //}

        //[HttpPost]
        //public ActionResult MyAccount(Customer customer)
        //{
        //    iCustomerRepository.create(customer);
        //    return RedirectToAction("Register","Account");
        //}

        [AllowAnonymous]
        public ActionResult Login()
        {
            ViewBag.uri = Request.ServerVariables["HTTP_REFERER"];
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login lg)
        {
            if (ModelState.IsValid)
            {
                using (DBWatchEntities dbwatch = new DBWatchEntities())
                {

                    
                    var admin = dbwatch.Admin.Where(a => a.username.Equals(lg.username) && a.password.Equals(lg.password)).FirstOrDefault();
                    if (admin != null)
                    {
                        //luu hien thi ten tren web
                        Session["userName"] = admin.username.ToString();
                        return RedirectToAction("Index","AdminBrands");
                        //return Redirect("JavaScript: history.go(-1)");\
                    }
                    else
                    {
                        ViewBag.Message = "Username or Password is invalid.";
                    }
                }
            }
            
            return View();
            
        }

        
        public ActionResult Logout()
        {
            //Session.Clear();
            //Session.Abandon();
            //return RedirectToAction("HomePage", "Home");
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
    }
}