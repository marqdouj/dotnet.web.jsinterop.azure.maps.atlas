using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Configuration;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Payloads
{
    /// <summary>
    /// Gets or sets the style applied to the map.
    /// </summary>
    public class StyleControlPayload
    {
        /// <summary>
        /// <inheritdoc cref="MapStyle"/>
        /// </summary>
        [JsonInclude] public MapStyle? Style { get; internal set; }
    }
}
