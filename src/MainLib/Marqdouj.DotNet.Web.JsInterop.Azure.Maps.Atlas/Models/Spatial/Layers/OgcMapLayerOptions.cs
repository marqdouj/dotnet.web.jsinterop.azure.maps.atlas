using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Layers;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Spatial.Layers
{
    /// <summary>
    /// Options for an <see cref="OgcMapLayer"/>.
    /// </summary>
    public class OgcMapLayerOptions : TileLayerOptions
    {
        /// <summary>
        /// Specifies the type of web mapping service to connect to.
        /// If not specified, will try and determine based on the URL.
        /// Defaults to 'WMTS' if not specified or if can't be determined from the URL.
        /// </summary>
        public OgcServiceType? Service { get; set; }

        /// <summary>The URL that points to the service.</summary>
        public string? Url { get; set; }

        /// <summary>Specifies the sublayers in the service to render. string | OgcSublayer.</summary>
        public List<object>? ActiveLayers { get; set; }

        /// <summary>The styles of the active layers. string | OgcStyle.</summary>
        public List<object>? Styles { get; set; }

        /// <summary>The version of the service to target. If not specified, will automatically try and determine this.</summary>
        public string? Version { get; set; }

        /// <summary>Indicates if the map should set the map view to the bounding box of the layer. Not persistent.</summary>
        public bool? BringIntoView { get; set; }

        /// <summary>Indicates if errors related to accessing the service should be logged to the console.</summary>
        public bool? Debug { get; set; }

        /// <summary>Custom dimensions to add to tile requests.</summary>
        public Dictionary<string, string>? Dimensions { get; set; }

        /// <summary>
        /// A URL to a proxy service that can have a URL to an external service appended it.
        /// This will be needed to access services that are hosted on non-CORs enabled endpoints.
        /// </summary>
        public string? ProxyService { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            var clone = (OgcMapLayerOptions)MemberwiseClone();
            return clone;
        }
    }
}
