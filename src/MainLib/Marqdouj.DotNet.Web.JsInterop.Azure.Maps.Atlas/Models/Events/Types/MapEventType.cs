using Marqdouj.DotNet.EnumConverters;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Types
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    /// <summary>
    /// Type of map event.
    /// </summary>
    [JsonConverter(typeof(LowerCaseEnumConverter<MapEventType>))]
    public enum MapEventType
    {
        Error,
        Ready,

        //Config
        MapConfigurationChanged,

        //Data
        Data,
        SourceData,
        StyleData,

        //General
        BoxZoomEnd,
        BoxZoomStart,
        Drag,
        DragEnd,
        DragStart,
        Idle,
        Load,
        Move,
        MoveEnd,
        MoveStart,
        Pitch,
        PitchEnd,
        PitchStart,
        Render,
        Resize,
        Rotate,
        RotateEnd,
        RotateStart,
        TokenAcquired,
        Zoom,
        ZoomEnd,
        ZoomStart,

        //layer
        LayerAdded,
        LayerRemoved,

        //Mouse
        Click,
        ContextMenu,
        DblClick,
        MouseDown,
        MouseMove,
        MouseOut,
        MouseOver,
        MouseUp,
        Wheel,

        //source
        SourceAdded,
        SourceRemoved,

        //style
        StyleChanged,
        StyleImageMissing,

        //Touch
        TouchCancel,
        TouchEnd,
        TouchMove,
        TouchStart,
    }

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
