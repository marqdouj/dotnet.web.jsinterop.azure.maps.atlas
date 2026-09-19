using Marqdouj.DotNet.EnumConverters;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Types
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    /// <summary>
    /// Subset of MapEventType that applies to MapEventTarget.htmlmarker. Castable to MapEventType.
    /// </summary>
    [JsonConverter(typeof(LowerCaseEnumConverter<MarkerEventType>))]
    public enum MarkerEventType
    {
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

        Drag,
        DragEnd,
        DragStart,

        //These events are listed in the Azure Maps SDK (and shown in examples),
        //however they are not supported directly by the API (and don't even work in the examples).
        //KeyDown,
        //KeyPress,
        //KeyUp,
    }

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
