using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace earthquakeprediction.Models
{
    public class EarthquakeModel
    {
        public double Magnitude { get; set; }
        public string Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime LocalDateTime { get; set; }
        public string Time { get; set; }
        public string Country { get; set; } // ✅ Added for filtering by country
    }
}
