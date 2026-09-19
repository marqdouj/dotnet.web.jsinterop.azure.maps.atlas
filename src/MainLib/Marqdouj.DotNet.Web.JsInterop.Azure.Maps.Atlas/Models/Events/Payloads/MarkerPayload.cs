using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Payloads
{
    /// <summary>
    /// Represents the payload data for an HTML marker event on a map, including event type and optional keyboard event
    /// information.
    /// </summary>
    public class MarkerPayload
    {
        /// <summary>
        /// The type of event.
        /// </summary>
        [JsonInclude]
        public string? Type { get; internal set; }
    }
}
