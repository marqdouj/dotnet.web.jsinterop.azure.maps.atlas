using Marqdouj.DotNet.EnumConverters;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.ImageSprite
{
    /// <summary>
    /// Image templates available within the Azure Maps Web SDK at the time of building this library.
    /// </summary>
    [JsonConverter(typeof(HyphenUnderscoreLCEnumConverter<ImageTemplateName>))]
    public enum ImageTemplateName
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Arrow_Up,
        Arrow_Up_Thin,
        Car,
        Checker,
        Checker_Rotated,
        Circles,
        Circles_Spaced,
        Diagonal_Lines_Down,
        Diagonal_Lines_Up,
        Diagonal_Stripes_Down,
        Diagonal_Stripes_Up,
        Dots,
        Flag,
        Flag_Triangle,
        Grid_Lines,
        Hexagon,
        Hexagon_Rounded,
        Hexagon_Rounded_Thick,
        Hexagon_Thick,
        Marker,
        Marker_Arrow,
        Marker_Ball_Pin,
        Marker_Circle,
        Marker_Flat,
        Marker_Square,
        Marker_Square_Cluster,
        Marker_Square_Rounded,
        Marker_Square_Rounded_Cluster,
        Marker_Thick,
        Pin,
        Pin_Round,
        Rotated_Grid_Lines,
        Rotated_Grid_Stripes,
        Rounded_Square,
        Rounded_Square_Thick,
        Triangle,
        Triangle_Arrow_Left,
        Triangle_Arrow_Up,
        Triangle_Thick,
        X_Fill,
        Zig_Zag,
        Zig_Zag_Vertical,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }

    /// <summary>
    /// Extension methods for <see cref="ImageTemplateName"/>
    /// </summary>
    public static class ImageTemplateNameExtensions
    {
        /// <summary>
        /// Replaces the '_' with a space.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDisplayName(this ImageTemplateName value) => value.ToString().Replace('_', ' ');

        /// <summary>
        /// Converts the value to it's map sdk name.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToJsonName(this ImageTemplateName value) => value.ToString().Replace('_', '-').ToLower();
    }
}
