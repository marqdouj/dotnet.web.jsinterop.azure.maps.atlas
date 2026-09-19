using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Types;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Definitions
{
    /// <summary>
    /// Map event definition.
    /// </summary>
    public class MapEvent(MapEventType type) : Event
    {
        /// <summary>
        /// <see cref="MapEventType"/>
        /// </summary>
        public MapEventType Type { get; } = type;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override MapEventTarget Target => MapEventTarget.Map;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
}
