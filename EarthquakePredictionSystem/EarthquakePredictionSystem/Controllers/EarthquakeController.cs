using System.Threading.Tasks;
using System.Web.Mvc;
using EarthquakePredictionSystem.Services;

namespace EarthquakePredictionSystem.Controllers
{
    public class EarthquakeController : Controller
    {
        public async Task<ActionResult> Recent()
        {
            var data = await EarthquakeService.GetRecentEarthquakes();
            return View(data);
        }

        // Endpoint returning JSON for map JavaScript
        public async Task<ActionResult> RecentJson()
        {
            var data = await EarthquakeService.GetRecentEarthquakes();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
