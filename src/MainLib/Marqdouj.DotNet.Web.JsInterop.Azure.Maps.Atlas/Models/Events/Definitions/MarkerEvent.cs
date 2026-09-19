using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Types;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Definitions
{
    /// <summary>
    /// Marker event definition.
    /// </summary>
    public class MarkerEvent(MarkerEventType type) : Event
    {
        /// <summary>
        /// <see cref="MarkerEventType"/>
        /// </summary>
        public MarkerEventType Type { get; } = type;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override MapEventTarget Target => MapEventTarget.Marker;

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
