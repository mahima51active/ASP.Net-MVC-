﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RegistrationForm.Controllers
{
    public class abcController : Controller
    {
        // GET: abc
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ashh()
        {
            ViewBag.nme = "assssssssss";
            return View();
        }
    }
}
