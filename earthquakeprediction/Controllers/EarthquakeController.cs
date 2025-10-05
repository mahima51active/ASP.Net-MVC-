using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using earthquakeprediction.Models;


namespace earthquakeprediction.Controllers
{
    public class EarthquakeController : Controller
    {
        // GET: Earthquake
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult indexx()
        {
            return View();
        }
        public ActionResult Recent()
        {
            var service = new EarthquakeService();
            var quakes = service.GetRecentEarthquakes()
                                .OrderByDescending(q => q.Magnitude)
                                .ToList();
            return View(quakes);
        }

        public JsonResult RecentJson()
        {
            var service = new EarthquakeService();
            var quakes = service.GetRecentEarthquakes()
                                .OrderByDescending(q => q.Magnitude)
                                .ToList();
            return Json(quakes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Graph()
        {
            return View();
        }

        public JsonResult GetEarthquakeGraphData()
        {
            var service = new EarthquakeService(); // your service to fetch data
            var data = service.GetRecentEarthquakes(); // include both past & predicted future

            var result = data.Select(q => new {
                //time = q.Time.ToString("yyyy-MM-dd"),
                time = DateTime.TryParse(q.Time, out var parsed) ? parsed.ToString("yyyy-MM-dd") : q.Time,
                magnitude = q.Magnitude,
                location = q.Location
            }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}