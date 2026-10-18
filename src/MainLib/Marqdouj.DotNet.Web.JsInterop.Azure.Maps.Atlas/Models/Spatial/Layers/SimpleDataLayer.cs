using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Sources;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Spatial.Layers
{
    /// <summary>
    /// <inheritdoc cref="SpatialLayerType.SimpleDataLayer"/>
    /// </summary>
    public class SimpleDataLayer : SpatialLayer
    {
        /// <summary>
        /// <inheritdoc cref="SimpleDataLayerOptions"/>
        /// </summary>
        public override object? Options 
        { 
            get; 
            set => field = value is SimpleDataLayerOptions ? value : value != null ? throw new ArgumentNullException(nameof(value), $"{nameof(SimpleDataLayer)} options expected value must be null or an instance of {nameof(SimpleDataLayerOptions)}.") : null;
        }

        /// <summary>
        /// Typed <see cref="Options"/>.
        /// </summary>
        [JsonIgnore]
        public SimpleDataLayerOptions? OptionsT { get => (SimpleDataLayerOptions?)Options; set => Options = value; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override SpatialLayerType Type => SpatialLayerType.SimpleDataLayer;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override object Clone()
        {
            var clone = (SimpleDataLayer)MemberwiseClone();

            if (Source is DataSource ds)
                clone.Source = ds.Clone();

            if (Options is SimpleDataLayerOptions options)
                clone.Options = options.Clone();

            return clone;
        }
    }
}
