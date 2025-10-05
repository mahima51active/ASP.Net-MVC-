using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace form.Controllers
{
    public class loginController : Controller
    {
        // GET: login
        public ActionResult Index()
        {
            if (Session["userid"] != null)
            {
                return RedirectToAction("Index", "Home"); // or Dashboard
            }
            else
            {
                return View();
            }
            
        }
        public ActionResult form()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Login(string userid, string password)
        {
            if (userid == "admin" && password == "arohi")
            {
                Session["userid"] = userid;
                Session["password"] = password;
                return Json(new { success = true, message = "Login successful" });
            }
            else
            {
                return Json(new { success = false, message = "Invalid credentials" });
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "login");
        }
    }
}