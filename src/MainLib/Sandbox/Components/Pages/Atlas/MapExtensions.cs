using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Configuration;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Controls;
using Marqdouj.DotNet.Web.JsInterop.GeoJson;

namespace Sandbox.Components.Pages.Atlas
{
    internal static class MapExtensions
    {
        public static List<MapControl> GetDefaultControls() =>
            [
                new FullscreenControl(),
                new ZoomControl(),
                new CompassControl(),
                new PitchControl(),
                new ScaleControl(),
                new StyleControl(),
                new TrafficControl(),
                new TrafficLegendControl(),
            ];

        public static MapOptions GetDefaultMapOptions(Position? center = null, double zoomLevel = 10.5)
        {
            // Initialize map options with a specific camera, style,and traffic options.
            return new MapOptions
            {
                Camera = new CameraOptions
                {
                    Center = center ?? new Position(-122.33, 47.6), // (Seattle, WA)
                    Zoom = zoomLevel,
                },
                Style = new StyleOptions { Style = MapStyle.road },
                Traffic = new TrafficOptions { Flow = TrafficFlow.none }
            };
        }
    }
}
