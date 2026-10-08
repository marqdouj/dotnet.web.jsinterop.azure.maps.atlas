using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Payloads.Animations
{
    /// <summary>
    /// Payload for a playable animation event.
    /// </summary>
    public sealed class PlayableAnimationPayload : PlayableAnimationBasePayload, IAsyncDisposable
    {
        /// <summary>
        /// If requested when creating the event, the PlayableAnimation will be included.
        /// Ensure that if the <see cref="IJSObjectReference"/> is included, it is properly disposed of when no longer needed.
        /// </summary>
        [JsonIgnore]
        public IJSObjectReference? Animation { get; internal set; }

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
