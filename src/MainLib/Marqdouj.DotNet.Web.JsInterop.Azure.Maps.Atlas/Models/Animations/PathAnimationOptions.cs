namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Animations
{
    /// <summary>
    /// Extends <see cref="PlayableAnimationOptions"/>
    /// </summary>
    public class PathAnimationOptions : PlayableAnimationOptions
    {
        /// <summary>
        /// Specifies if metadata should be captured as properties of the shape. 
        /// Potential metadata properties that may be captured: _heading
        /// </summary>
        public bool? CaptureMetadata { get; set; }

        /// <summary>
        /// Specifies if a curved geodesic path should be used between points rather than a straight pixel path.
        /// Default: is 'false'.
        /// </summary>
        public bool? Geodesic { get; set; }
    }
}
