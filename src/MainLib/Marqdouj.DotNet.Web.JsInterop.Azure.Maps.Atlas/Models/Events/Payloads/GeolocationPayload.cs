using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Types;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Geolocation;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Payloads
{
    /// <summary>
    /// Payload for a geolocation event.
    /// </summary>
    public class GeolocationPayload
    {
        /// <summary>
        /// <inheritdoc cref="GeolocationEventType"/>
        /// </summary>
        [JsonInclude]
        public GeolocationEventType Type { get; internal set; }

        /// <summary>
        /// <inheritdoc cref="GeolocationPosition"/>
        /// </summary>
        [JsonInclude]
        public GeolocationPosition? Position { get; internal set; }

        /// <summary>
        /// <inheritdoc cref="GeolocationPositionError"/>
        /// </summary>
        [JsonInclude]
        public GeolocationPositionError? Error { get; internal set; }
    }
}
