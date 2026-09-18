using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Types;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Definitions
{
    /// <summary>
    /// Layers event definition.
    /// </summary>
    public class LayerEvent(LayerEventType type) : Event
    {
        /// <summary>
        /// <see cref="LayerEventType"/>
        /// </summary>
        public LayerEventType Type { get; } = type;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override MapEventTarget Target => MapEventTarget.Layer;
    }
}
