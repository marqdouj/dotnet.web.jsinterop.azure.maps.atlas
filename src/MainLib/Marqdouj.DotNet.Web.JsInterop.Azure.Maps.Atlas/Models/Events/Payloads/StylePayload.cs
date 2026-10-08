using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Configuration;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Payloads
{
    /// <summary>
    /// Represents the payload for a map style event.
    /// </summary>
    public class StylePayload
    {
        /// <summary>
        /// <inheritdoc cref="MapStyle"/>
        /// </summary>
        [JsonInclude] public MapStyle? Style { get; internal set; }
    }
}
