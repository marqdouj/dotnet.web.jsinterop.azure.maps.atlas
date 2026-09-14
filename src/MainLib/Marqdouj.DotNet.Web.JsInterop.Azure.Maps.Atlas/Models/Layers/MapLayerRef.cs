using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Common;
using Microsoft.JSInterop;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Layers
{
    /// <summary>
    /// <see cref="IJSObjectReference"/> Wrapper to an atlas map layer.
    /// Ensure this object is disposed when done using the reference.
    /// </summary>
    public interface IMapLayerRef : IAsyncDisposable
    {
        /// <summary>
        /// <see cref="Map.MapId"/>
        /// </summary>
        string MapId { get; }

        /// <summary>
        /// <see cref="JsInteropIdBase.Id"/>
        /// </summary>
        string LayerId { get; }

        /// <summary>
        /// <see cref="LayerType"/>. 
        /// </summary>
        LayerType Type { get; }

        /// <summary>
        /// <see cref="IJSObjectReference"/> to the atlas map control.
        /// </summary>
        IJSObjectReference JsReference { get; }
    }

    internal class MapLayerRef : IMapLayerRef
    {
        public string MapId { get; set; } = default!;

        public string LayerId { get; } = default!;

        public LayerType Type { get; set; }

        public IJSObjectReference JsReference { get; set; } = default!;

        public async ValueTask DisposeAsync()
        {
            await JsReference.DisposeAsync();
        }
    }
}
