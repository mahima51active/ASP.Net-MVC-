using System;
 using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace earthquakeprediction.Models
{
    public class EarthquakeViewModel
    {
        public string SelectedCountry { get; set; }
        public List<string> CountryList { get; set; }
        public string Time { get; set; }
        public List<EarthquakeModel> EarthquakeResults { get; set; }
    }
}


//public class EarthquakeModel
//{
//    public string Location { get; set; } // From USGS "place"
//    public double Magnitude { get; set; }
//    public double Latitude { get; set; }
//    public double Longitude { get; set; }
//    public string Time { get; set; }
//    public DateTime LocalDateTime { get; set; }

//    public string Country  // NEW - extracted for view use
//    {
//        get
//        {
//            if (string.IsNullOrEmpty(Location)) return null;
//            var parts = Location.Split(',');
//            return parts.Length > 1 ? parts.Last().Trim() : null;
//        }
//    }

//    public string Place => Location;
//}
//public class EarthquakeModel
//{
//    public double Magnitude { get; set; }
//    public string Location { get; set; }
//    public string Country { get; set; } // ✅ Add this line
//    public double Latitude { get; set; }
//    public double Longitude { get; set; }
//    public string Time { get; set; }
//    public DateTime LocalDateTime { get; set; }
//}
