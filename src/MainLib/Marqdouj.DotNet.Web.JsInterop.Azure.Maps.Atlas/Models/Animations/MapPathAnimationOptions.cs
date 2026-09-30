using Microsoft.JSInterop;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Animations
{
    /// <summary>
    /// Extends <see cref="PathAnimationOptions"/>
    /// </summary>
    public class MapPathAnimationOptions : PathAnimationOptions
    {
        /// <summary>
        /// <see cref="IJSObjectReference"/> to an atlas.Map.
        /// </summary>
        public IJSObjectReference? Map { get; set; }

        /// <summary>
        /// A pitch value to set on the map. By default this is not set.
        /// </summary>
        public double? Pitch { get; set; }

        /// <summary>
        /// Specifies if the map should rotate such that the bearing of the map faces the direction the map is moving. 
        /// Default is 'true'.
        /// </summary>
        public bool? Rotate { get; set; }

        /// <summary>
        /// When rotate is set to true, the animation will follow the animation. 
        /// An offset of 180 will cause the camera to lead the animation and look back. 
        /// Default is '0'.
        /// </summary>
        public double? RotationOffset { get; set; } 

        /// <summary>
        /// A fixed zoom level to snap the map to on each animation frame. 
        /// By default the maps current zoom level is used.
        /// </summary>
        public double? Zoom { get; set; }
    }
}
