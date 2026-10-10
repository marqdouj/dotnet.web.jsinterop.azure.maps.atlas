using Marqdouj.DotNet.Web.JsInterop.GeoJson;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Spatial.Layers
{
    /// <summary>
    /// Sublayer information for OGC WMS and WMTS services.
    /// </summary>
    public class OgcSublayer
    {
        /// <summary>
        /// Unique identifier for the layer.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// The title for the layer.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// A description or abstract for the layer.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// A list of the supported styles.
        /// </summary>
        public List<OgcStyle>? Styles { get; set; }

        /// <summary>
        /// An integer specifying the minimum zoom level to render the layer at.
        /// </summary>
        public int? MinZoom { get; set; }

        /// <summary>
        /// An integer specifying the maximum zoom level to render the layer at.
        /// </summary>
        public int? MaxZoom { get; set; }

        /// <summary>
        /// The bounding box of the layer.
        /// </summary>
        public BoundingBox? Bounds { get; set; }

        /// <summary>
        /// Custom dimension values supported by the layer.
        /// </summary>
        public List<OgcDimension>? Dimensions { get; set; }
    }
}
