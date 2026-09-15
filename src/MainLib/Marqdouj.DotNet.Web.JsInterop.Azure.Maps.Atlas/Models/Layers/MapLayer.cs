using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Common;
using Microsoft.JSInterop;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Layers
{
    /// <summary>
    /// map layers supported by this library.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<LayerType>))]
    public enum LayerType
    {
        /// <summary>
        /// Renders point objects as scalable circles.
        /// </summary>
        Bubble,

        /// <summary>
        /// Represent the density of data using different colors.
        /// </summary>
        [Display(Name = "Heat map")]
        HeatMap,

        /// <summary>
        /// Overlays an image on the map with each corner anchored to a coordinate on the map. 
        /// Also known as a ground or image overlay.
        /// </summary>
        Image,

        /// <summary>
        /// Renders line data on the map. Can be used with SimpleLine, SimplePolygon,
        /// CirclePolygon, LineString, MultiLineString, Polygon, and MultiPolygon objects.
        /// </summary>
        Line,

        /// <summary>
        /// Renders filled Polygon and MultiPolygon objects on the map.
        /// </summary>
        Polygon,

        /// <summary>
        /// Renders extruded filled `Polygon` and `MultiPolygon` objects on the map.
        /// </summary>
        [Display(Name = "Polygon Extrusion")]
        PolygonExtrusion,

        /// <summary>
        /// Renders point based data as symbols on the map using text and/or icons.
        /// Symbols can also be created for line and polygon data as well.
        /// </summary>
        Symbol,

        /// <summary>
        /// Renders raster tiled images on top of the map tiles.
        /// </summary>
        Tile,
    }

    /// <summary>
    /// Base class for map layer.
    /// </summary>
    public abstract class MapLayer : JsInteropIdBase
    {
        /// <summary>
        /// <inheritdoc cref="LayerType"/>
        /// </summary>
        public abstract LayerType Type { get; }

        /// <summary>
        ///  Optionally specify a layer id to insert the new layer(s) before it.
        ///  Specify "labels" to place the new layer(s) just below the default label layer,
        ///  which will allow the labels to be visible on top of the custom layer.
        /// </summary>
        public string? Before { get; set; }

        /// <summary>
        /// The id or <see cref="IJSObjectReference"/> of a source which the layer will render.
        /// </summary>
        public object? Source { get; set; }
    }
}
