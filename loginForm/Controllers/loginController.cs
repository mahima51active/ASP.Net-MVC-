using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace loginForm.Controllers
{
    public class loginController : Controller
    {
        // GET: Default
        public ActionResult form()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Login(string userid, string password)
        {
            if (userid == "admin" && password == "arohi")
            {
                return Json(new { success = true, message = "Login successful" });
            }
            else
            {
                return Json(new { success = false, message = "Invalid credentials" });
            }
        }
    }
}