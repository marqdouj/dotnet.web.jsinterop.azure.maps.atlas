using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Common;
using Marqdouj.DotNet.Web.JsInterop.GeoJson;
using Microsoft.JSInterop;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Layers
{
    /// <summary>
    /// Defines the properties of a popup that can be displayed on the map.
    /// </summary>
    public class Popup : JsInteropIdBase
    {
        /// <summary>
        /// <inheritdoc cref="PopupOptions"/>
        /// </summary>
        public PopupOptions Options { get; set { ArgumentNullException.ThrowIfNull(field, nameof(Options)); field = value; } } = new();

        /// <summary>
        /// <inheritdoc cref="ICloneable"/>
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            var clone = (Popup)MemberwiseClone();
            clone.Options = (PopupOptions)Options.Clone();

            return clone;
        }
    }

    /// <summary>
    /// Options for configuring the behavior and appearance of a popup on the map.
    /// </summary>
    public class PopupOptions : ICloneable
    {
        /// <summary>
        /// Specifies if the popup can be dragged away from its position.
        /// default false
        /// </summary>
        public bool? Draggable { get; set; }

        /// <summary>
        /// Specifies if the close button should be displayed in the popup or not.
        /// default true
        /// </summary>
        public bool? CloseButton { get; set; }

        /// <summary>
        /// The content to display within the popup.
        /// <see cref="String"/> or <see cref="IJSObjectReference"/> to an HTMLElement.
        /// default span
        /// </summary>
        public object? Content { get; set; }

        /// <summary>
        /// Specifies the fill color of the popup.
        /// default "#FFFFFF"
        /// </summary>
        public string? FillColor { get; set; }

        /// <summary>
        /// How many pixels to the right and down the anchor of the popup should be offset.
        /// Negative numbers can be used to offset the popup left and up.
        /// default [0, 0]
        /// </summary>
        public Pixel? PixelOffset { get; set; }

        /// <summary>
        /// The position on the map where the popup should be anchored.
        /// default [0, 0]
        /// </summary>
        public Position? Position { get; set; }

        /// <summary>
        /// Specifies if the pointer should be displayed in the popup or not.
        /// default true
        /// </summary>
        public bool? ShowPointer { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            var clone = (PopupOptions)MemberwiseClone();
            clone.PixelOffset = PixelOffset?.Clone() as Pixel;
            clone.Position = Position?.Clone() as Position;

            return clone;
        }
    }
}
