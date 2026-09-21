using Microsoft.JSInterop;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Common
{
    /// <summary>
    /// <see cref="IJSObjectReference"/> Wrapper to an atlas map object.
    /// Ensure this object is disposed when done using the reference.
    /// </summary>
    public interface IMapObjectReference : IAsyncDisposable
    {
        /// <summary>
        /// <see cref="JsInteropIdBase.Id"/>
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// <see cref="IJSObjectReference"/> to the atlas map object.
        /// If null, then the map object was not found.
        /// </summary>
        IJSObjectReference? JsReference { get; set; }

        /// <summary>
        /// <see cref="Map.MapId"/>
        /// </summary>
        string MapId { get; set; }

        /// <summary>
        /// The type of map object.
        /// </summary>
        string? Type { get; set; }
    }

    internal sealed class MapObjectReference : IAsyncDisposable, IMapObjectReference
    {
        public string MapId { get; set; } = default!;

        public string Id { get; set; } = default!;

        public string? Type { get; set; }

        public IJSObjectReference? JsReference { get; set; } 

        public async ValueTask DisposeAsync()
        {
            if (JsReference != null)
            {
                await JsReference.DisposeAsync();
                JsReference = null;
            }
        }
    }

    public static class MapObjectReferenceExtensions
    {
        /// <summary>
        /// Disposes all references.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static async Task CleanUp(this List<IMapObjectReference>? items)
        {
            if (items == null) return;

            foreach (var item in items)
                await item.DisposeAsync();

            items.Clear();
        }
    }
}
