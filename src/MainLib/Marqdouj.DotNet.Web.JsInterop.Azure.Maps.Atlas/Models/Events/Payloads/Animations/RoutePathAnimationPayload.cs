using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Payloads.Animations
{
    /// <summary>
    /// Payload for a route path animation event.
    /// </summary>
    public sealed class RoutePathAnimationPayload : PlayableAnimationBasePayload, IAsyncDisposable
    {
        /// <summary>
        /// Average speed between points in meters per second.
        /// </summary>
        [JsonInclude] public double? Speed { get; internal set; }

        /// <summary>
        /// <see cref="Speed"/> converted to Kph.
        /// </summary>
        [JsonInclude] public double? SpeedInKph { get; internal set; }

        /// <summary>
        /// <see cref="Speed"/> converted to Mph.
        /// </summary>
        [JsonInclude] public double? SpeedInMph { get; internal set; }

        /// <summary>
        /// Estimated timestamp in the animation based on the timestamp information provided for each point.
        /// </summary>
        [JsonInclude] public double? Timestamp { get; internal set; }

        /// <summary>
        /// The <see cref="Timestamp"/> in UTC.
        /// </summary>
        [JsonInclude] public string? TimestampUTC { get; internal set; }

        /// <summary>
        /// If requested when creating the event, the PlayableAnimation will be included.
        /// Ensure that if the <see cref="IJSObjectReference"/> is included, it is properly disposed of when no longer needed.
        /// </summary>
        [JsonInclude]
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
