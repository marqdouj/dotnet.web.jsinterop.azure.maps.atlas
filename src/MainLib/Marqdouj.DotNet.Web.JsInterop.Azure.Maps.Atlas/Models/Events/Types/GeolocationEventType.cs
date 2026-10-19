using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Types
{
    /// <summary>
    /// Event Type that applies to a geolocation.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<GeolocationEventType>))]
    public enum GeolocationEventType
    {
        /// <summary>
        /// Geolocation location request.
        /// </summary>
        GetLocation,

        /// <summary>
        /// Watch was successful.
        /// </summary>
        WatchSuccess,

        /// <summary>
        /// Watch generated an error.
        /// </summary>
        WatchError,
    }
}
