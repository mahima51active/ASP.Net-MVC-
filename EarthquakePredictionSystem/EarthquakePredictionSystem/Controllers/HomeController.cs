using System.Web.Mvc;

namespace EarthquakePredictionSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
