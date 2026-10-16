using Marqdouj.DotNet.EnumConverters;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Types
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    /// <summary>
    /// Event Type that applies to animations.
    /// </summary>
    [JsonConverter(typeof(LowerCaseEnumConverter<AnimationEventType>))]
    public enum AnimationEventType
    {
        OnComplete,
        OnFrame,
        OnProgress,
    }

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
