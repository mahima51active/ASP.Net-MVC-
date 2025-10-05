using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace loginregistrationform.Controllers
{
    public class loginController : Controller
    {
         
        // GET: login
        public ActionResult login()
        {   
            return View();
        }
        [HttpPost]
        public ActionResult login(string username, string password)
        {
            if (username == "admin" && password == "123")
            {
                ViewBag.Message = "Login Successful!";
                Session["userid"] = username;
                Session["password"] = password;
                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewBag.Message = "Invalid Credentials!";
            }
            return View();
        }
    }
}