using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace AverageCalculation.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        // Optional GET: Result page
        public ActionResult Result()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            // 3 trainees x 3 rounds
            int[,] oxygenLevels = new int[3, 3];

            // Read values from form manually
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    string key = $"oxygenLevels[{i}][{j}]";  // Field name
                    int value = 0;
                    int.TryParse(form[key], out value);
                    oxygenLevels[i, j] = value;
                }
            }

            // Calculate averages
            double[] averages = new double[3];
            for (int i = 0; i < 3; i++)
            {
                averages[i] = (oxygenLevels[i, 0] + oxygenLevels[i, 1] + oxygenLevels[i, 2]) / 3.0;
            }

            // Find highest average
            double highestAvg = averages.Max();

            // Collect trainees with highest average
            StringBuilder fitTrainees = new StringBuilder();
            for (int i = 0; i < 3; i++)
            {
                if (Math.Abs(averages[i] - highestAvg) < 0.0001)
                {
                    fitTrainees.AppendLine($"Trainee {i + 1} (Avg: {averages[i]:0.00})<br/>");
                }
            }

            ViewBag.HighestAvg = highestAvg.ToString("0.00");
            ViewBag.FitTrainees = fitTrainees.ToString();

            return View("Result");
        }
    }
}
