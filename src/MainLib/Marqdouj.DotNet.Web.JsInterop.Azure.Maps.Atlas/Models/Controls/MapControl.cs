using Marqdouj.DotNet.EnumConverters;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Common;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Controls
{
    /// <summary>
    /// Type of map control.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<ControlType>))]
    public enum ControlType
    {
        /// <summary>
        /// A control for changing the rotation of the map.
        /// </summary>
        Compass,

        /// <summary>
        /// A control to make the map or a specified element full screen.
        /// </summary>
        Fullscreen,

        /// <summary>
        /// A control for changing the pitch of the map.
        /// </summary>
        Pitch,

        /// <summary>
        /// A control to display a scale bar on the map.
        /// </summary>
        Scale,

        /// <summary>
        /// A control for changing the style of the map.
        /// </summary>
        Style,

        /// <summary>
        /// A control for displaying traffic on the map.
        /// </summary>
        Traffic,

        /// <summary>
        /// A control for displaying traffic legend on the map.
        /// </summary>
        TrafficLegend,

        /// <summary>
        /// A control for changing the zoom of the map.
        /// </summary>
        Zoom,
    }

    /// <summary>
    /// Style for a map control.
    /// </summary>
    [JsonConverter(typeof(LowerCaseEnumConverter<ControlStyle>))]
    public enum ControlStyle
    {
        /// <summary>
        /// The control will be in the light style.
        /// </summary>
        Light,

        /// <summary>
        /// The control will be in the dark style.
        /// </summary>
        Dark,

        /// <summary>
        /// The control will automatically switch styles based on the style of the map.
        /// If a control doesn't support automatic styling the light style will be used by default.
        /// </summary>
        Auto
    }

    /// <summary>
    /// Position where the control is to be placed on the map.
    /// </summary>
    [JsonConverter(typeof(HyphenUnderscoreLCEnumConverter<ControlPosition>))]
    public enum ControlPosition
    {
        /// <summary>
        ///The control will place itself in its default location.
        /// </summary>
        Non_Fixed,

        /// <summary>
        ///Places the control in the top left of the map.
        /// </summary>
        Top_Left,

        /// <summary>
        ///Places the control in the top right of the map.
        /// </summary>
        Top_Right,

        /// <summary>
        ///Places the control in the bottom left of the map.
        /// </summary>
        Bottom_Left,

        /// <summary>
        ///Places the control in the bottom right of the map.
        /// </summary>
        Bottom_Right,
    }

    /// <summary>
    /// Base class for a map control.
    /// </summary>
    public abstract class MapControl : JsInteropIdBase
    {
        /// <summary>
        /// <see cref="ControlType"/>
        /// </summary>
        public abstract ControlType Type { get; }

        /// <summary>
        /// <see cref="Controls.ControlOptions"/>
        /// </summary>
        public ControlOptions? ControlOptions { get; set; }
    }

    /// <summary>
    /// The options for adding a control to the map.
    /// </summary>
    public class ControlOptions : ICloneable
    {
        /// <summary>
        /// The position the control will be placed on the map. 
        /// </summary>
        public ControlPosition? Position { get; set; }

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
