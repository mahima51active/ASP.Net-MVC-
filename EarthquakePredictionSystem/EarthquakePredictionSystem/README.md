# Earthquake Prediction System (ASP.NET MVC)

**Target:** .NET Framework 4.8 • Visual Studio 2022  
**Features**
- Fetches live earthquake data (last 24h) from USGS feed.
- Displays table with magnitude, location, local time.
- Interactive Leaflet map with color + size coded markers.
- JSON endpoint: `/Earthquake/RecentJson` for AJAX/map.

> NOTE: Real earthquake *prediction* is not solved scientifically. This demo focuses on visualization. You can later add heuristic "risk" summaries.

## Getting Started
1. Open `EarthquakePredictionSystem.sln` in VS 2022.
2. Let NuGet restore packages (Microsoft.AspNet.Mvc, Newtonsoft.Json etc.).
3. Press **F5** (IIS Express).
4. Navigate to `/Earthquake/Recent` to view map + table.

## Extend Ideas
- Add risk logic (group by region & average magnitude).
- Cache API response for X minutes to reduce external calls.
- Add filters: min magnitude, time range.
- Add email/SMS alert when magnitude >= threshold.
- Persist snapshots in a database for historical charts.

## Files Overview
- `Services/EarthquakeService.cs`: HTTP fetch + parse.
- `Controllers/EarthquakeController.cs`: MVC actions + JSON endpoint.
- `Views/Earthquake/Recent.cshtml`: Table + Leaflet map.
- `Views/Shared/_Layout.cshtml`: Bootstrap + Leaflet includes.
- `Models/*`: Data transfer + deserialization classes.

## Change Feed Source
Edit URL inside `EarthquakeService.cs`:
- All day: `all_day.geojson`
- Past hour: `all_hour.geojson`
- M2.5+: `2.5_day.geojson`, etc.

See: https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php

## Simple Risk (Idea)
Group events by country/region string part and compute average magnitude; label High (>=6), Medium (>=4), Low.

Enjoy coding!
