namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Controls
{
    /// <summary>
    /// <see cref="ControlType.Traffic"/>
    /// </summary>
    public class TrafficControl : MapControl
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"><see cref="ControlPosition"/></param>
        /// <param name="options"><see cref="TrafficControlOptions"/></param>
        public TrafficControl(ControlPosition? position = ControlPosition.Top_Right, TrafficControlOptions? options = null)
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
        public override ControlType Type => ControlType.Traffic;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public TrafficControlOptions? Options { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            var clone = (TrafficControl)MemberwiseClone();
            clone.ControlOptions = (ControlOptions?)ControlOptions?.Clone();
            clone.Options = (TrafficControlOptions?)Options?.Clone();

            return clone;
        }
    }

    /// <summary>
    /// The options for a TrafficControl.
    /// </summary>
    public class TrafficControlOptions : ICloneable
    {
        /// <summary>
        /// The style of the control.
        /// Default is 'light'.
        /// </summary>
        public ControlStyle? Style { get; set; }

        /// <summary>
        /// Specifies if the control is in the active state (displaying traffic).
        /// </summary>
        public bool? IsActive { get; set; }

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
