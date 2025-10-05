using System.Threading.Tasks;
using System.Web.Mvc;
using EarthquakePredictionSystemClassic.Services;

namespace EarthquakePredictionSystemClassic.Controllers
{
    public class EarthquakeController : Controller
    {
        public async Task<ActionResult> Recent()
        {
            var data = await EarthquakeService.GetRecentEarthquakes();
            return View(data);
        }

        public async Task<ActionResult> RecentJson()
        {
            var data = await EarthquakeService.GetRecentEarthquakes();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
