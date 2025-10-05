using System.Collections.Generic;

namespace EarthquakePredictionSystem.Models
{
    // Classes to deserialize USGS GeoJSON response
    public class USGSGeoJsonResponse
    {
        public List<Feature> Features { get; set; }
    }

    public class Feature
    {
        public FeatureProperties Properties { get; set; }
        public FeatureGeometry Geometry { get; set; }
    }

    public class FeatureProperties
    {
        public double? Mag { get; set; }
        public string Place { get; set; }
        public long Time { get; set; }
    }

    public class FeatureGeometry
    {
        public List<double> Coordinates { get; set; } // [longitude, latitude, depth]
    }
}
