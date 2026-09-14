namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Controls
{
    /// <summary>
    /// <see cref="ControlType.Fullscreen"/>
    /// </summary>
    public class FullscreenControl : MapControl
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"><see cref="ControlPosition"/></param>
        /// <param name="options"><see cref="FullscreenControlOptions"/></param>
        public FullscreenControl(ControlPosition? position = ControlPosition.Top_Right, FullscreenControlOptions? options = null)
        {
            if (position != null)
            {
                ControlOptions ??= new ControlOptions();
                ControlOptions.Position = position;
            }
            Options = options;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override ControlType Type => ControlType.Fullscreen;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public FullscreenControlOptions? Options { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            var clone = (FullscreenControl)MemberwiseClone();
            clone.ControlOptions = (ControlOptions?)ControlOptions?.Clone();
            clone.Options = (FullscreenControlOptions?)Options?.Clone();

            return clone;
        }
    }

    /// <summary>
    /// The options for a FullscreenControl.
    /// </summary>
    public class FullscreenControlOptions : ICloneable
    {
        /// <summary>
        /// The style of the control.
        /// Default is 'light'.
        /// </summary>
        public ControlStyle? Style { get; set; }

        /// <summary>
        /// Id of the HTML element which should be made full screen.
        /// If not specified, the map container element will be used.
        /// </summary>
        public string? ContainerId { get; set; }

        /// <summary>
        /// Indicates if the control should be hidden if the browser does not support full screen mode.
        /// Default is 'false'
        /// </summary>
        public bool? HideIfUnsupported { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
