using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Types;
using Marqdouj.DotNet.Web.JsInterop.GeoJson;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Payloads.Animations
{
    /// <summary>
    /// Base class for playable animations.
    /// </summary>
    public abstract class PlayableAnimationBasePayload
    {
        /// <summary>
        /// The raw event type.
        /// </summary>
        [JsonInclude]
        public string Type { get; internal set; } = default!;

        /// <summary>
        /// Attempts to parse the <see cref="Type"/> into an <inheritdoc cref="AnimationEventType"/>
        /// </summary>
        [JsonIgnore]
        public AnimationEventType? EventType
        {
            get => Enum.TryParse(Type, true, out AnimationEventType result) ? result : default;
        }

        /// <summary>
        /// Progress of the animation where 0 is the start and 1 is the end.
        /// </summary>
        [JsonInclude]
        public double Progress { get; internal set; }

        /// <summary>
        /// The progress of the animation after being passed through an easing function.
        /// </summary>
        [JsonInclude]
        public double? EasingProgress { get; internal set; }

        /// <summary>
        /// The focal position of an animation frame. Returned by path animations.
        /// </summary>
        [JsonInclude]
        public Position? Position { get; internal set; }

        /// <summary>
        /// The focal heading of an animation frame. Returned by path animations.
        /// </summary>
        [JsonInclude]
        public double? Heading { get; internal set; }
    }
}
