using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Common;
using Marqdouj.DotNet.Web.JsInterop.GeoJson;
using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Spatial.Layers
{
    /// <summary>
    /// The spatial layer types supported by this library.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<SpatialLayerType>))]
    public enum SpatialLayerType
    {
        /// <summary>
        /// A layer that simplifies the rendering of geospatial data on the map.
        /// **Note:** Because this layer wraps other layers which will be added/removed with this
        /// some layer ordering operations are not supported.
        /// Adding this layer before another, adding another layer before this, and moving this layer are not supported.
        /// These restrictions only apply to this layer and not the layers wrapped by this.
        /// </summary>
        SimpleDataLayer,

        /// <summary>
        /// Renders raster tiled images on top of the map tiles from an OGC Web Mapping Service (WMS or WMTS).
        /// </summary>
        OgcMapLayer,
    }

    /// <summary>
    /// Base class for spatial layers.
    /// </summary>
    public abstract class SpatialLayer : JsInteropIdBase
    {
        /// <summary>
        ///  Optionally specify a layer id to insert the new layer(s) before it.
        ///  Specify "labels" to place the new layer(s) just below the default label layer,
        ///  which will allow the labels to be visible on top of the custom layer.
        ///  NOTE: Not all spatial layers support this feature.
        /// </summary>
        public string? Before { get; set; }

        /// <summary>
        /// A property for associating custom data with the layer.
        /// Normally this would be <see cref="GeoJsonProperties"/>.
        /// </summary>
        public object? Metadata { get; set; }

        /// <summary>
        /// The options associated with the <see cref="SpatialLayerType"/>.
        /// </summary>
        public abstract object? Options { get; set; }

        /// <summary>
        /// The id or <see cref="IJSObjectReference"/> of a source which the layer will render.
        /// </summary>
        public object? Source { get; set; }

        /// <summary>
        /// <inheritdoc cref="SpatialLayerType"/>
        /// </summary>
        public abstract SpatialLayerType Type { get; }
    }
}
