using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Layers;
using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules
{
    /// <summary>
    /// Interface for map markers.
    /// </summary>
    public interface IAtlasMarkers
    {
        /// <summary>
        /// Add marker to the map.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        ValueTask Add(Map map, HtmlMarker item);

        /// <summary>
        /// Add markers to the map.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        ValueTask Add(Map map, IEnumerable<HtmlMarker> items);

        /// <summary>
        /// Clear the markers from the map.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        ValueTask Clear(Map map);

        /// <summary>
        /// Remove marker from the map.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        ValueTask Remove(Map map, HtmlMarker item);

        /// <summary>
        /// Remove markers from the map.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        ValueTask Remove(Map map, IEnumerable<HtmlMarker> items);
    }

    internal class AzMarkers(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasMarkers
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;

        public async ValueTask Add(Map map, HtmlMarker item)
        {
            await Add(map, [item]);
        }

        public async ValueTask Add(Map map, IEnumerable<HtmlMarker> items)
        {
            await moduleTask.InvokeVoidAsyncInternal(GetJsInteropMethod(), map.MapReference, items?.Cast<object>().ToList());
        }

        public async ValueTask Clear(Map map)
        {
            await moduleTask.InvokeVoidAsyncInternal(GetJsInteropMethod(), map.MapReference);
        }

        public async ValueTask Remove(Map map, HtmlMarker item)
        {
            await Remove(map, [item]);
        }

        public async ValueTask Remove(Map map, IEnumerable<HtmlMarker> items)
        {
            await moduleTask.InvokeVoidAsyncInternal(GetJsInteropMethod(), map.MapReference, items?.Cast<object>().ToList());
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.Markers.GetJsModuleMethod(name);
    }
}
