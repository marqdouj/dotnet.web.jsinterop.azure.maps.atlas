using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Types;
using Microsoft.JSInterop;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Definitions
{
    /// <summary>
    /// Animation event definition.
    /// </summary>
    public class AnimationEvent(AnimationEventType type) : Event
    {
        /// <summary>
        /// <see cref="AnimationEventType"/>
        /// </summary>
        public AnimationEventType Type { get; } = type;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override MapEventTarget Target => MapEventTarget.Animation;

        /// <summary>
        /// If true, the <see cref="IJSObjectReference"/> for the event will be included.
        /// </summary>
        public bool IncludeJsReference { get; set; }

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
