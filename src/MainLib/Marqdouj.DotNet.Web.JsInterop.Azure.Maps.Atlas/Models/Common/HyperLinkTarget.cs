using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Common
{
    /// <summary>
    /// Specifies the where the hyperlink should open.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<HyperLinkTarget>))]
    public enum HyperLinkTarget
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        _blank,
        _self,
        _parent,
        _top,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
