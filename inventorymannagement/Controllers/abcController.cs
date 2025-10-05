using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace inventorymannagement.Controllers
{
    public class abcController : Controller
    {
        // GET: abc
        public ActionResult dashboard()
        {
            if (Session["userid"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("login", "login");
            }
        }

       
    }
}