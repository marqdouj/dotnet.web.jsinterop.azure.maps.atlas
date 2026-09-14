using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Converters;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Layers
{
    /// <summary>
    /// <see cref="LayerType.Bubble"/>
    /// </summary>
    public class BubbleLayer : MapLayer
    {
        /// <summary>
        /// <inheritdoc cref="LayerType"/>
        /// </summary>
        public override LayerType Type => LayerType.Bubble;

        /// <summary>
        /// <inheritdoc cref="BubbleLayerOptions"/>
        /// </summary>
        public BubbleLayerOptions? Options { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            var clone = (BubbleLayer)MemberwiseClone();
            clone.Options = (BubbleLayerOptions?)MemberwiseClone();
            return clone;
        }
    }

    /// <summary>
    /// Specifies the orientation of circle when map is pitched.
    /// </summary>
    [JsonConverter(typeof(LowerCaseEnumConverter<BubbleLayerPitchAlignment>))]
    public enum BubbleLayerPitchAlignment
    {
        /// <summary>
        /// The circle is aligned to the plane of the map.
        /// </summary>
        Map,

        /// <summary>
        /// The circle is aligned to the plane of the viewport.
        /// </summary>
        Viewport,
    }

    /// <summary>
    /// Options used when rendering Point objects in a BubbleLayer.
    /// </summary>
    public class BubbleLayerOptions : SourceLayerOptions
    {
        /// <summary>
        /// The color to fill the circle symbol with.
        /// String or DataDrivenPropertyValueSpecification.
        /// Default "#1A73AA" (dark Blue).
        /// </summary>
        public object? Color { get; set; }

        /// <summary>
        /// The amount to blur the circles.
        /// A value of 1 blurs the circles such that only the center point is at full opacity.
        /// Number or DataDrivenPropertyValueSpecification.
        /// Default '0'.
        /// </summary>
        public object? Blur { get; set; }

        /// <summary>
        /// A number between 0 and 1 that indicates the opacity at which the circles will be drawn.
        /// Number or DataDrivenPropertyValueSpecification.
        /// Default '1'.
        /// </summary>
        public object? Opacity { get; set; }

        /// <summary>
        /// The color of the circles' outlines.
        /// String or DataDrivenPropertyValueSpecification.
        /// Default '#FFFFFF'.
        /// </summary>
        public object? StrokeColor { get; set; }

        /// <summary>
        /// A number between 0 and 1 that indicates the opacity at which the circles' outlines will be drawn.
        /// Number or DataDrivenPropertyValueSpecification.
        /// Default '1'.
        /// </summary>
        public object? StrokeOpacity { get; set; }

        /// <summary>
        /// The width of the circles' outlines in pixels.
        /// Number or DataDrivenPropertyValueSpecification.
        /// Default '2'.
        /// </summary>
        public object? StrokeWidth { get; set; }

        /// <summary>
        /// Specifies the orientation of circle when map is pitched.
        /// "map": The circle is aligned to the plane of the map.
        /// "viewport": The circle is aligned to the plane of the viewport.
        /// Default 'viewport'
        /// </summary>
        public BubbleLayerPitchAlignment? PitchAlignment { get; set; }

        /// <summary>
        /// The radius of the circle symbols in pixels.
        /// Must be greater than or equal to 0.
        /// Number or DataDrivenPropertyValueSpecification.
        /// Default '8'.
        /// </summary>
        public object? Radius { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            var clone = (BubbleLayerOptions)MemberwiseClone();
            return clone;
        }
    }
}
