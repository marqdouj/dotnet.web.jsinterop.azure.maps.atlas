using Marqdouj.DotNet.EnumConverters;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Animations
{
    /// <summary>
    /// The set of supported interpolation strategies.
    /// </summary>
    [JsonConverter(typeof(LowerCaseEnumConverter<PointInterpolation>))]
    public enum PointInterpolation
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Linear,
        Nearest,
        Min,
        Max,
        Avg
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
