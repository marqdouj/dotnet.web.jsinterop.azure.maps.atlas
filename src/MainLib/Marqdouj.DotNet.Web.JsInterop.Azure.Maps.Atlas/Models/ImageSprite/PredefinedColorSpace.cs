using Marqdouj.DotNet.EnumConverters;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.ImageSprite
{
    /// <summary>
    /// The color space of the image data.
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/API/ImageData/colorSpace"/>
    /// </summary>
    [JsonConverter(typeof(LowerCaseEnumConverter<PredefinedColorSpace>))]
    public enum PredefinedColorSpace
    {
        /// <summary>
        /// <see href="https://en.wikipedia.org/wiki/SRGB"/>
        /// </summary>
        SRgb,
        /// <summary>
        /// <see href="https://en.wikipedia.org/wiki/DCI-P3"/>
        /// </summary>
        Display_P3,
    }
}

