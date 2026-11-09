using Marqdouj.DotNet.Web.JsInterop.GeoJson;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Sources
{
    /// <summary>
    /// General information about a shape in a <see cref="DataSource"/>.
    /// </summary>
    public class ShapeInfo
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [JsonInclude]
        public string Id { get; internal set; } = default!;

        /// <summary>
        /// Type of shape.
        /// </summary>
        [JsonInclude]
        public string Type { get; internal set; } = default!;

        /// <summary>
        /// Gets the <see cref="BoundingBox"/> of the shape.
        /// </summary>
        [JsonInclude]
        public BoundingBox Bounds { get; internal set; } = default!;

        /// <summary>
        /// Gets the <see cref="GeoJsonProperties"/> of the shape.
        /// </summary>
        [JsonInclude]
        public GeoJsonProperties? Properties { get; internal set; }
    }
}
