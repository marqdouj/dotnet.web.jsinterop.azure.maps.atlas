using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Types;
using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Payloads.Animations
{
    /// <summary>
    /// Payload for a frame-based animation event.
    /// </summary>
    public sealed class FrameBasedAnimationPayload : IAsyncDisposable
    {
        /// <summary>
        /// The event type.
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
        /// If requested when creating the event, the FrameBasedAnimationTimer will be included.
        /// Ensure that if the <see cref="IJSObjectReference"/> is included, it is properly disposed of when no longer needed.
        /// </summary>
        [JsonInclude]
        public IJSObjectReference? Animation { get; internal set; }

        /// <summary>
        /// The index of the frame if using the frame based animation timer.
        /// </summary>
        [JsonInclude]
        public double? FrameIdx { get; internal set; }

        /// <summary>
        /// The number of frames in the animation.
        /// </summary>
        [JsonInclude]
        public double? NumFrames { get; internal set; }

        /// <summary>
        /// Disposes of the <see cref="Animation"/> <see cref="IJSObjectReference"/> if it is not null.
        /// </summary>
        /// <returns></returns>
        public async ValueTask DisposeAsync()
        {
            if (Animation != null)
                await Animation.DisposeAsync();
        }
    }
}
