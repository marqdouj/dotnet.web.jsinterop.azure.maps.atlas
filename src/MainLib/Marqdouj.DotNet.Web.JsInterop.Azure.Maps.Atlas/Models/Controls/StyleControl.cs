using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Converters;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Configuration;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Controls
{
    /// <summary>
    /// <see cref="ControlType.Style"/>
    /// </summary>
    public class StyleControl : ControlBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"><see cref="ControlPosition"/></param>
        /// <param name="options"><see cref="StyleControlOptions"/></param>
        public StyleControl(ControlPosition? position = ControlPosition.Top_Right, StyleControlOptions? options = null)
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
        public override ControlType Type => ControlType.Style;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public StyleControlOptions? Options { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            var clone = (StyleControl)MemberwiseClone();
            clone.ControlOptions = (ControlOptions?)ControlOptions?.Clone();
            clone.Options = (StyleControlOptions?)Options?.Clone();

            return clone;
        }
    }

    /// <summary>
    /// The layout to display the styles in.
    /// </summary>
    [JsonConverter(typeof(LowerCaseEnumConverter<StyleControlLayout>))]
    public enum StyleControlLayout
    {
        /// <summary>
        /// A row of clickable icons for each style.
        /// </summary>
        Icons,

        /// <summary>
        /// A scrollable list with the icons and names for each style.
        /// </summary>
        List,
    }

    /// <summary>
    /// The options for a StyleControl.
    /// </summary>
    public class StyleControlOptions : ICloneable
    {
        /// <summary>
        /// The style of the control.
        /// Default is 'light'.
        /// </summary>
        public ControlStyle? Style { get; set; }

        /// <summary>
        /// The layout to display the styles in.
        /// Default 'icons'
        /// </summary>
        public StyleControlLayout? Layout { get; set; }

        /// <summary>
        /// The map styles to show in the control.
        /// Default = road, Grayscale (light), Grayscale (dark)", Night, Terra
        /// </summary>
        public List<MapStyle>? MapStyles { get; set => field = value is null || value.Count == 0 ? null : value; }

        /// <summary>
        /// Whether to let style control automatically set the style, once user selects a map style.
        /// If set to 'false', then clicking on style will not set the set the style automatically.
        /// Default is 'true'
        /// </summary>
        public bool? AutoSelectionMode { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            var clone = (StyleControlOptions)MemberwiseClone();
            clone.MapStyles = MapStyles == null ? null : [.. MapStyles];

            return clone;
        }
    }
}
