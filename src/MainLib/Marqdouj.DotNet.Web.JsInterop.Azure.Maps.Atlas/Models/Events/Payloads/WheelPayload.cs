using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Payloads
{
    /// <summary>
    /// Payload for the wheel event on the map.
    /// </summary>
    public class WheelPayload
    {
        /// <summary>
        /// Wheel event type.
        /// </summary>
        [JsonInclude] public string? Type { get; internal set; }
    }
}
