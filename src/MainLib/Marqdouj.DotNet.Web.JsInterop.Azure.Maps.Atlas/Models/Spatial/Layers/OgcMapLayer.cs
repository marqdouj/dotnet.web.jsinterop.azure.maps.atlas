using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Sources;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Spatial.Layers
{
    /// <summary>
    /// <inheritdoc cref="SpatialLayerType.OgcMapLayer"/>
    /// </summary>
    public class OgcMapLayer : SpatialLayer
    {
        /// <summary>
        /// <inheritdoc cref="OgcMapLayerOptions"/>
        /// </summary>
        public override object? Options
        {
            get;
            set => field = value is OgcMapLayerOptions ? value : value != null ? throw new ArgumentNullException(nameof(value), $"{nameof(OgcMapLayer)} options expected value must be null or an instance of {nameof(OgcMapLayerOptions)}.") : null;
        }

        /// <summary>
        /// Typed <see cref="Options"/>.
        /// </summary>
        [JsonIgnore]
        public OgcMapLayerOptions? OptionsT { get => (OgcMapLayerOptions?)Options; set => Options = value; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override SpatialLayerType Type => SpatialLayerType.OgcMapLayer;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override object Clone()
        {
            var clone = (OgcMapLayer)MemberwiseClone();

            if (Source is DataSource ds)
                clone.Source = ds.Clone();

            if (Options is OgcMapLayerOptions options)
                clone.Options = options.Clone();

            return clone;
        }
    }
}
