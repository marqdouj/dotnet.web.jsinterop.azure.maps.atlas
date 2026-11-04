namespace Sandbox.Services
{
    public class SDKRawUrls
    {
        internal static string BaseUrl => "https://raw.githubusercontent.com/Azure-Samples/AzureMapsCodeSamples/refs/heads/main/Static/data";

        public GeoJsonUrls GeoJson { get; } = new();
        public GeoRSS GeoRSS { get; } = new();
        public GpxUrls Gpx { get; } = new();
        public KmlUrls Kml { get; } = new();
    }

    public class GeoJsonUrls
    {
        public RouteNetworksUrls RouteNetworks { get; } = new();
    }

    public class GeoRSS
    {
        private readonly string baseUrl = Path.Combine(SDKRawUrls.BaseUrl, "GeoRSS");

        public string SampleGeoRSS => Path.Combine(baseUrl, "SampleGeoRSS.xml");
        public string WifiLocations => Path.Combine(baseUrl, "WifiLocations.xml");
        public string Eqs7day_M2_5 => Path.Combine(baseUrl, "eqs7day-M2.5.xml");

        public List<string> GetNames() => this.GetPropertyNames();
    }

    public class GpxUrls
    {
        private readonly string baseUrl = Path.Combine(SDKRawUrls.BaseUrl, "Gpx");

        public string BikeRoute => Path.Combine(baseUrl, "BikeRoute.xml");
        public string Route66Attractions => Path.Combine(baseUrl, "Route66Attractions.xml");
        public string TouristLocationsUKEngland => Path.Combine(baseUrl, "Tourist_locations_UK-England.xml");
        public string GpsPoints => Path.Combine(baseUrl, "gps_points.xml");
        public string KuhkopfsteigFv => Path.Combine(baseUrl, "kuhkopfsteig-fv.gpx");
        public string RaceGpsTrace => Path.Combine(baseUrl, "race-gps-trace.gpx");

        public List<string> GetNames() => this.GetPropertyNames();
    }

    public class KmlUrls
    {
        private readonly string baseUrl = Path.Combine(SDKRawUrls.BaseUrl, "Kml");

        public string InternetUsers2005Choropleth3D => Path.Combine(baseUrl, "internet_users_2005_choropleth_3D.kml");

        public List<string> GetNames() => this.GetPropertyNames();
    }

    public class RouteNetworksUrls
    {
        private readonly string baseUrl = Path.Combine(SDKRawUrls.BaseUrl, "geojson/route-networks");
        
        public string IpswichRoadNetwork => Path.Combine(baseUrl, "Ipswich-Road-Network.json");
        public string MaritimeTradeRoutes => Path.Combine(baseUrl, "maritime-trade-routes.geojson");
        public string MontrealRoads => Path.Combine(baseUrl, "montreal_roads.json");
        public string NiagaraRoads => Path.Combine(baseUrl, "niagra_roads.json");
        public string TransitRoutesDataSD => Path.Combine(baseUrl, "transit_routes_datasd.geojson");

        public List<string> GetNames() => this.GetPropertyNames();
    }

    internal static class SDKRawUrlsExtensions
    {
        public static List<string> GetPropertyNames(this object obj)
        {
            return [.. obj.GetType().GetProperties().Select(p => p.Name).OrderBy(n => n)];
        }
    }
}
