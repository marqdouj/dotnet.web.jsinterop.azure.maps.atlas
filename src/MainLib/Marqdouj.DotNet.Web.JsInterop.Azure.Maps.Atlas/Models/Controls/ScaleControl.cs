using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Converters;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Controls
{
    /// <summary>
    /// <see cref="ControlType.Scale"/>
    /// </summary>
    public class ScaleControl : MapControl
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"><see cref="ControlPosition"/></param>
        /// <param name="options"><see cref="ScaleControlOptions"/></param>
        public ScaleControl(ControlPosition? position = ControlPosition.Bottom_Right, ScaleControlOptions? options = null)
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
        public override ControlType Type => ControlType.Scale;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public ScaleControlOptions? Options { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            var clone = (ScaleControl)MemberwiseClone();
            clone.ControlOptions = (ControlOptions?)ControlOptions?.Clone();
            clone.Options = (ScaleControlOptions?)Options?.Clone();

            return clone;
        }
    }

    /// <summary>
    /// Unit of the distance.
    /// </summary>
    [JsonConverter(typeof(LowerCaseEnumConverter<ScaleControlUnit>))]
    public enum ScaleControlUnit
    {
        /// <summary>
        /// 
        /// </summary>
        Metric,

        /// <summary>
        /// 
        /// </summary>
        Imperial,

        /// <summary>
        /// 
        /// </summary>
        Nautical,
    }

    /// <summary>
    /// The options for a ScaleControl.
    /// </summary>
    public class ScaleControlOptions : ICloneable
    {
        /// <summary>
        /// The style of the control.
        /// Default is 'light'.
        /// </summary>
        public ControlStyle? Style { get; set; }

        /// <summary>
        /// The maximum length of the scale control in pixels.
        /// Default is '100'
        /// </summary>
        public double? MaxWidth { get; set; }

        /// <summary>
        /// Unit of the distance.
        /// Default is <see cref="ScaleControlUnit.Metric"/>.
        /// </summary>
        public ScaleControlUnit? Unit { get; set; }

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
