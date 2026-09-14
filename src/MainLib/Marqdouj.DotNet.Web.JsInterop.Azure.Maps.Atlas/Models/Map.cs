using Microsoft.JSInterop;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models
{
    /// <summary>
    /// <see cref="IJSObjectReference"/> Wrapper to an atlas Map.
    /// Ensure this class is disposed when done using the map.
    /// </summary>
    public sealed class Map : IAsyncDisposable
    {
        internal Map(IJSObjectReference map, string mapId)
        {
            MapReference = map;
            MapId = mapId;
        }

        /// <summary>
        /// <see cref="IJSObjectReference"/> to the atlas.Map
        /// </summary>
        public IJSObjectReference MapReference { get; }

        /// <summary>
        /// The id of the html element where the map is displayed.
        /// </summary>
        public string MapId { get; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public async ValueTask DisposeAsync()
        {
            await MapReference.DisposeAsync();
        }
    }
}
