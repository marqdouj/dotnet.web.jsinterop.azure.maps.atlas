using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Common;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Sources
{
    /// <summary>
    /// The source type used for a layer.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<SourceType>))]
    public enum SourceType
    {
        /// <summary>
        /// A source that makes it easy to manage shape data that will be displayed on the map.
		/// It may be used with the SymbolLayer, LineLayer, PolygonLayer, BubbleLayer, and HeatMapLayer.
        /// </summary>
        Data,

        /// <summary>
        /// A source that describes how to access elevation (raster DEM) tile data.
        /// </summary>
        ElevationTile,

        /// <summary>
        /// A source that provides vector tiles for rendering on the map.
        /// </summary>
        VectorTile,
    }

    /// <summary>
    /// Base class for a map layer source.
    /// </summary>
    public abstract class SourceBase : JsInteropIdBase
    {
        /// <summary>
        /// <inheritdoc cref="SourceType"/>
        /// </summary>
        public abstract SourceType? Type { get; }
    }
}
