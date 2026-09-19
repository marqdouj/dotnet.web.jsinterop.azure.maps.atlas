using Marqdouj.DotNet.EnumConverters;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Types
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    /// <summary>
    /// Subset of MapEventType that applies to MapEventTarget.shape. Castable to MapEventType.
    /// </summary>
    [JsonConverter(typeof(LowerCaseEnumConverter<ShapeEventType>))]
    public enum ShapeEventType
    {
        ShapeChanged,
    }

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
