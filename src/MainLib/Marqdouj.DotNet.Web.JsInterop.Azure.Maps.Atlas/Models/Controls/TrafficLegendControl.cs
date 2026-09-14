namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Controls
{
    /// <summary>
    /// <see cref="ControlType.TrafficLegend"/>
    /// </summary>
    public class TrafficLegendControl : ControlBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"><see cref="ControlPosition"/></param>
        /// <param name="options"><see cref="TrafficLegendControlOptions"/></param>
        public TrafficLegendControl(ControlPosition? position = ControlPosition.Top_Right, TrafficLegendControlOptions? options = null)
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
        public override ControlType Type => ControlType.TrafficLegend;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public TrafficLegendControlOptions? Options { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            var clone = (TrafficLegendControl)MemberwiseClone();
            clone.ControlOptions = (ControlOptions?)ControlOptions?.Clone();
            clone.Options = (TrafficLegendControlOptions?)Options?.Clone();

            return clone;
        }
    }

    /// <summary>
    /// The options for a TrafficLegendControl.
    /// NOTE: Placeholder, this control does not currently support any options.
    /// </summary>
    public class TrafficLegendControlOptions : ICloneable
    {
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
