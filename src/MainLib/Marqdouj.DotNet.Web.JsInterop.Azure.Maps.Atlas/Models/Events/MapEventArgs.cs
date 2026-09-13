using Marqdouj.DotNet.Web.JsInterop.GeoJson;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    /// <summary>
    /// Specifies the target element for map-related events.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<MapEventTarget>))]
    public enum MapEventTarget
    {
        map,
    }

    /// <summary>
    /// Specifies the type of map-related events.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<MapEventTarget>))]
    public enum MapEventType
    {
        error,
        ready,
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

    /// <summary>
    /// Arguments returned from a map-related event.
    /// </summary>
    public class MapEventArgs
    {
        /// <summary>
        /// The id of the html element where the map is displayed.
        /// </summary>
        public string MapId { get; set; } = default!;

        /// <summary>
        /// <see cref="MapEventTarget"/>
        /// </summary>
        public MapEventTarget Target { get; set; } = default!;

        /// <summary>
        /// <see cref="MapEventType"/>
        /// </summary>
        public MapEventType Type { get; set; } = default!;

        /// <summary>
        /// <see cref="GeoJsonProperties"/>
        /// </summary>
        public GeoJsonProperties? Payload { get; set; }

        /// <summary>
        /// Tries to get a value from the Payload.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>The item or null if not found.</returns>
        public object? GetPayloadItem(string key)
        {
            if (Payload?.TryGetValue(key, out var payloadItem) ?? false)
                return payloadItem;

            return null;
        }
    }
}
