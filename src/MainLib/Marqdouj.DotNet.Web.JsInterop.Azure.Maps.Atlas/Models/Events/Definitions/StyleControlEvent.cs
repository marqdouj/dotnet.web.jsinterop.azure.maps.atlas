using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Types;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Definitions
{
    /// <summary>
    /// StyleControl event definition.
    /// </summary>
    public class StyleControlEvent(StyleControlEventType type) : Event
    {
        /// <summary>
        /// <see cref="StyleControlEventType"/>
        /// </summary>
        public StyleControlEventType Type { get; } = type;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override MapEventTarget Target => MapEventTarget.StyleControl;

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
