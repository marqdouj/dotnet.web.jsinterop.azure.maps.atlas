namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.ImageSprite
{
    /// <summary>
    /// Represents the underlying pixel data of an area of a canvas element.
    /// </summary>
    /// <param name="colorSpace"><see cref="PredefinedColorSpace"/></param>
    /// <param name="data"><see cref="Data"/></param>
    /// <param name="width"><see cref="Width"/></param>
    /// <param name="height">M<see cref="Height"/></param>
    public class ImageData(PredefinedColorSpace colorSpace, byte[] data, int width, int height)
    {
        /// <summary>
        /// Color space of the image data.
        /// </summary>
        public PredefinedColorSpace ColorSpace { get; } = colorSpace;

        /// <summary>
        /// Pixel data.
        /// </summary>
        public byte[] Data { get; } = data ?? throw new ArgumentNullException(nameof(data));

        /// <summary>
        /// Number of rows in the ImageData object.
        /// </summary>
        public int Height { get; } = height;

        /// <summary>
        /// Number of pixels per row in the ImageData object.
        /// </summary>
        public int Width { get; } = width;
    }
}

