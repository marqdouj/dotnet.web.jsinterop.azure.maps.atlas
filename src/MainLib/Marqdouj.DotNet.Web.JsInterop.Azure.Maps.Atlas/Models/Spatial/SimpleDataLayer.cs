using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Common;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Sources;
using Microsoft.JSInterop;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Spatial
{
    /// <summary>
    /// A layer that simplifies the rendering of geospatial data on the map.
    /// **Note:** Because this layer wraps other layers which will be added/removed with this
    /// some layer ordering operations are not supported.
    /// Adding this layer before another, adding another layer before this, and moving this layer are not supported.
    /// These restrictions only apply to this layer and not the layers wrapped by this.
    /// </summary>
    public class SimpleDataLayer : JsInteropIdBase
    {
        /// <summary>
        /// <inheritdoc cref="SimpleDataLayerOptions"/>
        /// </summary>
        public SimpleDataLayerOptions? Options { get; set; }

        /// <summary>
        /// The id or <see cref="IJSObjectReference"/> of a source which the layer will render.
        /// </summary>
        public object? Source { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override object Clone()
        {
            var clone = (SimpleDataLayer)MemberwiseClone();

            if (Source is DataSource ds)
                clone.Source = ds.Clone();

            clone.Options = Options?.Clone() as SimpleDataLayerOptions;
            return clone;
        }
    }
}
