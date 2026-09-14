namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Controls
{
    /// <summary>
    /// <see cref="ControlType.Zoom"/>
    /// </summary>
    public class ZoomControl : ControlBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"><see cref="ControlPosition"/></param>
        /// <param name="options"><see cref="ZoomControlOptions"/></param>
        public ZoomControl(ControlPosition? position = ControlPosition.Top_Right, ZoomControlOptions? options = null)
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
        public override ControlType Type => ControlType.Zoom;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public ZoomControlOptions? Options { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            var clone = (ZoomControl)MemberwiseClone();
            clone.ControlOptions = (ControlOptions?)ControlOptions?.Clone();
            clone.Options = (ZoomControlOptions?)Options?.Clone();

            return clone;
        }
    }

    /// <summary>
    /// The options for a ZoomControl.
    /// </summary>
    public class ZoomControlOptions : ICloneable
    {
        /// <summary>
        /// The style of the control.
        /// Default is 'light'.
        /// </summary>
        public ControlStyle? Style { get; set; }

        /// <summary>
        /// The extent to which the map will zoom with each click of the control.
        /// Default is '1'.
        /// </summary>
        public double ZoomDelta { get; set; } = 1;

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
