using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using EarthquakePredictionSystem.Models;

namespace EarthquakePredictionSystem.Services
{
    public static class EarthquakeService
    {
        private static readonly HttpClient _http = new HttpClient();

        public static async Task<List<EarthquakeModel>> GetRecentEarthquakes()
        {
            string url = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
            var json = await _http.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<USGSGeoJsonResponse>(json);

            var list = data.Features
                .Where(f => f?.Properties?.Mag != null)
                .Select(f => new EarthquakeModel
                {
                    Magnitude = f.Properties.Mag ?? 0,
                    Location = f.Properties.Place,
                    Time = f.Properties.Time,
                    Longitude = f.Geometry.Coordinates[0],
                    Latitude = f.Geometry.Coordinates[1]
                })
                .OrderByDescending(e => e.Magnitude)
                .ToList();

            return list;
        }
    }
}
