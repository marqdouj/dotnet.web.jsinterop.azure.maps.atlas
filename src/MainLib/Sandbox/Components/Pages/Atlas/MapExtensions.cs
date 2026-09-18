using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Configuration;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Controls;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Payloads;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Types;
using Marqdouj.DotNet.Web.JsInterop.GeoJson;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sandbox.Components.Pages.Atlas
{
    internal static class MapExtensions
    {
        private static readonly JsonSerializerOptions jsonMinOptions = new()
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        extension<T>(T obj)
        {
            internal string ToJsonMin()
            {
                return JsonSerializer.Serialize(obj, jsonMinOptions);
            }
        }


        public static async Task<bool> ErrorEventProcessed(this MapEventArgs e, ILogger logger, INotificationService toastService)
        {
            if (e.Target == MapEventTarget.Map && e.TypeToEnum<MapEventType>() == MapEventType.Error)
            {
                var error = e.GetPayloadItem<ErrorPayload>(nameof(MapEventType.Error));
                string msg = $"Map Error. Id: {e.MapId}, Message: {error?.BuildMessage()}";
                logger.LogError(msg);
                await toastService.ShowInfo(msg);
                return true;
            }

            return false;
        }

        public static async Task<bool> ErrorEventNotProcessed(this MapEventArgs e, ILogger logger, INotificationService toastService)
        {
            return (await e.ErrorEventProcessed(logger, toastService)) == false;
        }

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
