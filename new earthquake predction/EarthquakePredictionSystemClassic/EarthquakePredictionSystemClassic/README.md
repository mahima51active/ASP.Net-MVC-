# Earthquake Visualization - ASP.NET MVC 5 (.NET Framework 4.8)

**Features**
- Classic ASP.NET MVC 5 project (NOT .NET Core).
- Fetches real-time earthquakes (USGS last 24h feed).
- Displays table + Leaflet map.
- JSON endpoint: /Earthquake/RecentJson

## Run
1. Open `EarthquakePredictionSystemClassic.sln` in VS 2022.
2. NuGet restore (packages.config).
3. Press F5.
4. Go to /Earthquake/Recent

## Extend
- Min magnitude filter.
- Caching results for 10 minutes (MemoryCache).
- Alerts for magnitude >= threshold.
- Risk summary logic.

Enjoy!
