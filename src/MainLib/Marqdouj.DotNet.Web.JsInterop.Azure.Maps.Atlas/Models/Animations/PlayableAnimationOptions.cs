using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Animations
{
    /// <summary>
    /// Options for playing animations.
    /// </summary>
    public class PlayableAnimationOptions
    {
        /// <summary>
        /// The duration of the animation in ms. Default: 1000 ms
        /// </summary>
        public int? Duration { get; set; }

        /// <summary>
        /// Specifies if the animation should start automatically or wait for the play function to be called. Default: false
        /// </summary>
        public bool? AutoPlay { get; set; }

        /// <summary>
        /// Easing function name.
        /// </summary>
        public string? Easing { get; set; }

        /// <summary>
        /// Updates the <see cref="Easing"/> with an <inheritdoc cref="Animations.EasingName"/>.
        /// </summary>
        [JsonIgnore]
        public EasingName? EasingName 
        { 
            get 
            {
                if (Enum.TryParse<EasingName>(Easing, true, out var name))
                    return name;

                return null;
            }
            set
            {
                Easing = value?.ToString()?.ToJsonName();
            }
        }

        /// <summary>
        /// Specifies if the animation should loop infinitely. Default: false
        /// </summary>
        public bool? Loop { get; set; }

        /// <summary>
        /// Specifies if the animation should play backwards. Default: false
        /// </summary>
        public bool? Reverse { get; set; }

        /// <summary>
        /// A multiplier of the duration to speed up or down the animation. Default: 1
        /// </summary>
        public double? SpeedMultiplier { get; set; }

        /// <summary>
        /// Specifies if the animation should dispose itself once it has completed. Default: false
        /// </summary>
        public bool? DisposeOnComplete { get; set; }
    }
}
