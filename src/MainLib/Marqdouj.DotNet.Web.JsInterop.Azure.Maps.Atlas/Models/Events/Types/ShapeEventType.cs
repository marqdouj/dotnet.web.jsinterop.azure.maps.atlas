using Marqdouj.DotNet.EnumConverters;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Types
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    /// <summary>
    /// Event Type that applies to a Shape.
    /// </summary>
    [JsonConverter(typeof(LowerCaseEnumConverter<ShapeEventType>))]
    public enum ShapeEventType
    {
        ShapeChanged,
    }

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
