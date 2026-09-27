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
        marker,
        marker_thick,
        marker_circle,
        pin,
        pin_round,
        marker_flat,
        marker_arrow,
        marker_ball_pin,
        marker_square,
        marker_square_cluster,
        marker_square_rounded,
        marker_square_rounded_cluster,
        flag,
        flag_triangle,
        rounded_square,
        rounded_square_thick,
        triangle,
        triangle_thick,
        hexagon,
        hexagon_thick,
        hexagon_rounded,
        hexagon_rounded_thick,
        triangle_arrow_up,
        triangle_arrow_left,
        arrow_up,
        arrow_up_thin,
        car,
        checker,
        checker_rotated,
        zig_zag,
        zig_zag_vertical,
        circles_spaced,
        circles,
        diagonal_lines_up,
        diagonal_lines_down,
        diagonal_stripes_up,
        diagonal_stripes_down,
        grid_lines,
        rotated_grid_lines,
        rotated_grid_stripes,
        x_fill,
        dots,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
