using Marqdouj.DotNet.EnumConverters;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Payloads.Animations;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Payloads
{
    /// <summary>
    /// The type of animation event payload.
    /// </summary>
    [JsonConverter(typeof(CamelCaseEnumConverter<AnimationPayloadType>))]
    public enum AnimationPayloadType
    {
        /// <summary>
        /// A playable animation event.
        /// </summary>
        Playable,

        /// <summary>
        /// A frame-based animation event.
        /// </summary>
        FrameBased,

        /// <summary>
        /// A route path animation event.
        /// </summary>
        RoutePath,
    }

    /// <summary>
    /// Payload for an animation event.
    /// </summary>
    public sealed class AnimationPayload : IAsyncDisposable
    {
        /// <summary>
        /// The event type.
        /// </summary>
        [JsonInclude]
        public string Type { get; internal set; } = default!;

        /// <summary>
        /// Attempts to parse the <see cref="Type"/> into an <inheritdoc cref="AnimationPayloadType"/>
        /// </summary>
        [JsonIgnore]
        public AnimationPayloadType? PayloadType
        {
            get => Enum.TryParse(Type, true, out AnimationPayloadType result) ? result : default;
        }

        /// <summary>
        /// <inheritdoc cref="FrameBasedAnimationPayload"/>
        /// </summary>
        [JsonInclude]
        public FrameBasedAnimationPayload? FrameBased { get; internal set; }

        /// <summary>
        /// <inheritdoc cref="PlayableAnimationPayload"/>
        /// </summary>
        [JsonInclude]
        public PlayableAnimationPayload? Playable { get; internal set; }

        /// <summary>
        /// <inheritdoc cref="RoutePathAnimationPayload"/>
        /// </summary>
        [JsonInclude] 
        public RoutePathAnimationPayload? RoutePath { get; internal set; }

        /// <summary>
        /// Disposes of the <see cref="FrameBased"/>, <see cref="Playable"/> and <see cref="RoutePath"/> payloads if they are not null.
        /// </summary>
        /// <returns></returns>
        public async ValueTask DisposeAsync()
        {
            if (FrameBased != null)
                await FrameBased.DisposeAsync();
            if (Playable != null)
                await Playable.DisposeAsync();
            if (RoutePath != null)
                await RoutePath.DisposeAsync();
        }
    }
}
