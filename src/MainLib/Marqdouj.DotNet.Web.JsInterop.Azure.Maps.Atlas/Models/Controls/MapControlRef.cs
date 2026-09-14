using Microsoft.JSInterop;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Controls
{
    /// <summary>
    /// <see cref="IJSObjectReference"/> Wrapper to an atlas map control.
    /// Ensure this object is disposed when done using the reference.
    /// </summary>
    public interface IMapControlRef : IAsyncDisposable
    {
        /// <summary>
        /// <see cref="Map.MapId"/>
        /// </summary>
        string MapId { get; }

        /// <summary>
        /// <see cref="ControlType"/>. 
        /// If null, then the control type could not be resolved (i.e. custom control not added by this library).
        /// </summary>
        ControlType? Type { get; }

        /// <summary>
        /// <see cref="IJSObjectReference"/> to the atlas map control.
        /// </summary>
        IJSObjectReference JsReference { get; }
    }

    internal class MapControlRef : IMapControlRef
    {
        public string MapId { get; set; } = default!;

        public ControlType? Type { get; set; }

        public IJSObjectReference JsReference { get; set; } = default!;

        public async ValueTask DisposeAsync()
        {
            await JsReference.DisposeAsync();
        }
    }
}
