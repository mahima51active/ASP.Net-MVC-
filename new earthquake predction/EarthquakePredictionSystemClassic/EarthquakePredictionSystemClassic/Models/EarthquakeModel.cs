using System;

namespace EarthquakePredictionSystemClassic.Models
{
    public class EarthquakeModel
    {
        public double Magnitude { get; set; }
        public string Location { get; set; }
        public long Time { get; set; } // Unix ms
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public DateTime LocalDateTime => DateTimeOffset.FromUnixTimeMilliseconds(Time).LocalDateTime;
    }
}
