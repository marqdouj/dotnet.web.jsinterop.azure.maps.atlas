using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Configuration;

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
        public MapStyle? Style { get; set; }
    }
}
