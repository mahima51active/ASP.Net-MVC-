using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace form.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            if (Session["userid"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index","login");
            }
        }


        public ActionResult About()
        {
            if (Session["userid"] != null)
            {
                ViewBag.Message = "Your application description page.";
                return View();
            }
            else
            {
                return RedirectToAction("Index", "login");
            }
           
        }

        public ActionResult Contact()
        {        
            if (Session["userid"] != null)
            {
                ViewBag.Message = "Your contact page.";

                return View();
            }
            else
            {
                return RedirectToAction("Index", "login");
            }
        }
    }
}