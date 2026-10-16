using Marqdouj.DotNet.EnumConverters;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Types
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    /// <summary>
    /// Event Type that applies to a Layer.
    /// </summary>
    [JsonConverter(typeof(LowerCaseEnumConverter<LayerEventType>))]
    public enum LayerEventType
    {
        LayerAdded,
        LayerRemoved,

        Click,
        ContextMenu,
        DblClick,
        MouseDown,
        MouseEnter,
        MouseLeave,
        MouseMove,
        MouseOut,
        MouseOver,
        MouseUp,

        TouchCancel,
        TouchEnd,
        TouchMove,
        TouchStart,

        Wheel,
    }

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
