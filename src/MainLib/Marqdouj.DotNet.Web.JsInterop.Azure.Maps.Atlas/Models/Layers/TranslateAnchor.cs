using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Converters;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Layers
{
    /// <summary>
    /// Specifies the frame of reference for `translate`.
    /// </summary>
    [JsonConverter(typeof(LowerCaseEnumConverter<TranslateAnchor>))]
    public enum TranslateAnchor
    {
        /// <summary>
        /// Translate relative to the map.
        /// </summary>
        Map,

        /// <summary>
        /// Translate relative to the viewport
        /// </summary>
        Viewport,
    }
}
