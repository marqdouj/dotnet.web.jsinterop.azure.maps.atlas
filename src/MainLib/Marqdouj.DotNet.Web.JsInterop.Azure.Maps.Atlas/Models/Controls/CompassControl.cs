namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Controls
{
    /// <summary>
    /// <see cref="ControlType.Compass"/>
    /// </summary>
    public class CompassControl : ControlBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"><see cref="ControlPosition"/></param>
        /// <param name="options"><see cref="CompassControlOptions"/></param>
        public CompassControl(ControlPosition? position = ControlPosition.Top_Right, CompassControlOptions? options = null)
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
        public override ControlType Type => ControlType.Compass;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public CompassControlOptions? Options { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            var clone = (CompassControl)MemberwiseClone();
            clone.ControlOptions = (ControlOptions?)ControlOptions?.Clone();
            clone.Options = (CompassControlOptions?)Options?.Clone();

            return clone;
        }
    }

    /// <summary>
    /// The options for a CompassControl.
    /// </summary>
    public class CompassControlOptions : ICloneable
    {
        /// <summary>
        /// The style of the control.
        /// Default is 'light'.
        /// </summary>
        public ControlStyle? Style { get; set; }

        /// <summary>
        /// The angle that the map will rotate with each click of the control.
        /// Default is '15'.
        /// </summary>
        public double RotationDegreesDelta { get; set; } = 15;

        /// <summary>
        /// Inverts the direction of map rotation controls.
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
