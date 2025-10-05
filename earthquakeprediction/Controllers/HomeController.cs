using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using earthquakeprediction.Models;

namespace earthquakeprediction.Controllers
{
    public class HomeController : Controller
    {
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult Index(string SelectedCountry)
        {
            EarthquakeService service = new EarthquakeService();
            var allQuakes = service.GetRecentEarthquakes();

            foreach (var quake in allQuakes)
            {
                if (!string.IsNullOrEmpty(quake.Location))
                {
                    var parts = quake.Location.Split(',');
                    quake.Country = parts.Length > 1 ? parts.Last().Trim() : "Unknown";
                }
                else
                {
                    quake.Country = "Unknown";
                }
            }

            var countries = allQuakes
                .Where(q => !string.IsNullOrEmpty(q.Country))
                .Select(q => q.Country)
                .Distinct()
                .OrderBy(c => c)
                .ToList();

            var filteredQuakes = string.IsNullOrEmpty(SelectedCountry) || SelectedCountry == "All Countries"
                ? allQuakes
                : allQuakes.Where(q => q.Country == SelectedCountry).ToList();

            EarthquakeViewModel model = new EarthquakeViewModel
            {
                SelectedCountry = SelectedCountry,
                CountryList = countries,
                EarthquakeResults = filteredQuakes
            };

            return View(model);
        }

        private string ExtractCountry(string place)
        {
            if (string.IsNullOrEmpty(place))
                return null;

            // Assuming country is at the end after the last comma
            var parts = place.Split(',');
            return parts.Length > 1 ? parts.Last().Trim() : null;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}