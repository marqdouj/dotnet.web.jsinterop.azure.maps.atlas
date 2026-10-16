namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Spatial.Layers
{
    /// <summary>
    /// OGC WMS and WMTS layer style information.
    /// </summary>
    public class OgcStyle
    {
        /// <summary>
        /// The identifier of the style.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// The title of the style.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// A URL to the legend graphic.
        /// </summary>
        public string? LegendUrl { get; set; }

        /// <summary>
        /// Indicates if the style is the default.
        /// </summary>
        public bool? IsDefault { get; set; }
    }
}
