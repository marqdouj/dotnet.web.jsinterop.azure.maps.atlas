using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Configuration;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Models.Events
{
    /// <summary>
    /// Gets or sets the style applied to the map.
    /// </summary>
    public class StyleControlPayload
    {
        /// <summary>
        /// <inheritdoc cref="MapStyle"/>
        /// </summary>
        public MapStyle? Style { get; set; }
    }
}
