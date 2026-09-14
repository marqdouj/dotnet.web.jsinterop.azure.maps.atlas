namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Controls
{
    /// <summary>
    /// <see cref="ControlType.Pitch"/>
    /// </summary>
    public class PitchControl : MapControl
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"><see cref="ControlPosition"/></param>
        /// <param name="options"><see cref="PitchControlOptions"/></param>
        public PitchControl(ControlPosition? position = ControlPosition.Top_Right, PitchControlOptions? options = null)
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
        public override ControlType Type => ControlType.Pitch;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public PitchControlOptions? Options { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            var clone = (PitchControl)MemberwiseClone();
            clone.ControlOptions = (ControlOptions?)ControlOptions?.Clone();
            clone.Options = (PitchControlOptions?)Options?.Clone();

            return clone;
        }
    }

    /// <summary>
    /// The options for a PitchControl.
    /// </summary>
    public class PitchControlOptions : ICloneable
    {
        /// <summary>
        /// The style of the control.
        /// Default is 'light'.
        /// </summary>
        public ControlStyle? Style { get; set; }

        /// <summary>
        /// The angle that the map will tilt with each click of the control.
        /// Default is '10'.
        /// </summary>
        public double PitchDegreesDelta { get; set; } = 10;

        /// <summary>
        /// Inverts the direction of map pitch controls.
        /// Default is 'false'.
        /// </summary>
        public bool? Inverted { get; set; }

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
