using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace earthquakeprediction.Models
{
    public class EarthquakeService
    {

        private const string USGS_URL = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";

        public List<EarthquakeModel> GetRecentEarthquakes()
        {
            List<EarthquakeModel> quakes = new List<EarthquakeModel>();
            using (var client = new HttpClient())
            {

                var response = client.GetStringAsync(USGS_URL).Result;
                var json = JObject.Parse(response);

                foreach (var feature in json["features"])
                {
                    var prop = feature["properties"];
                    var coords = feature["geometry"]["coordinates"];
                    long timeUnix = (long)prop["time"];
                    DateTime timeUtc = DateTimeOffset.FromUnixTimeMilliseconds(timeUnix).UtcDateTime;

                    quakes.Add(new EarthquakeModel
                    {
                        Magnitude = (double?)prop["mag"] ?? 0.0,
                        Location = (string)prop["place"],
                        Latitude = (double)coords[1],
                        Longitude = (double)coords[0],
                        LocalDateTime = timeUtc.ToLocalTime(),
                        Time = timeUtc.ToLocalTime().ToString()
                    });
                }
            }
            return quakes;
        }
        
    }

   
    }