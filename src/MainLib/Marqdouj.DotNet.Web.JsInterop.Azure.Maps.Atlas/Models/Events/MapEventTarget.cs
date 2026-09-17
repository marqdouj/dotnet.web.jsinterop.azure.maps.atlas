using Marqdouj.DotNet.EnumConverters;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    /// <summary>
    /// Specifies the target element for map-related events.
    /// </summary>
    [JsonConverter(typeof(LowerCaseEnumConverter<MapEventTarget>))]
    public enum MapEventTarget
    {
        Map,
        Animation,
        DataSource,
        HtmlMarker,
        Layer,
        Popup,
        Shape,
        StyleControl,
    }

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
