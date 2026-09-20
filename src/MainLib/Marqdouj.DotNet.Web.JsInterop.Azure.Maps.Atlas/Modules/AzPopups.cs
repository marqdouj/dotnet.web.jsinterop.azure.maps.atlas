using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Common;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Layers;
using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules
{
    /// <summary>
    /// Interface for popups.
    /// </summary>
    public interface IAtlasPopups
    {
        /// <summary>
        /// Add a popup.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="items"></param>
        /// <param name="getReferences">
        /// If true, then return the <see cref="IMapObjectReference"/> for the popups. Default is false.
        /// IMPORTANT! Ensure the <see cref="IMapObjectReference"/> is disposed when you are done with it.
        /// </param>
        /// <returns></returns>
        ValueTask<List<IMapObjectReference>> Add(Map map, IEnumerable<Popup> items, bool getReferences = false);

        /// <summary>
        /// Add popups.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="item"></param>
        /// <param name="getReferences">
        /// If true, then return the <see cref="IMapObjectReference"/> for the popups. Default is false.
        /// IMPORTANT! Ensure the <see cref="IMapObjectReference"/> items are disposed when you are done with them.
        /// </param>
        /// <returns></returns>
        ValueTask<List<IMapObjectReference>> Add(Map map, Popup item, bool getReferences = false);

        /// <summary>
        /// Adds a hover popup to the map.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="layer"></param>
        /// <param name="item"></param>
        /// <param name="placeholders">Shape/Feature property names used to update the template.</param>
        /// <param name="getReference">
        /// If true, then return the <see cref="IMapObjectReference"/> for the popup. Default is false.
        /// IMPORTANT! Ensure the <see cref="IMapObjectReference"/> is disposed when you are done with it.
        /// </param>
        /// <returns></returns>
        ValueTask<IMapObjectReference?> AddHoverPopup(Map map, ILayer layer, Popup item, IEnumerable<string> placeholders, bool getReference = false);

        /// <summary>
        /// Gets the <see cref="IMapObjectReference"/> for the popup.
        /// IMPORTANT! Ensure the <see cref="IMapObjectReference"/> is disposed when you are done with it.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        ValueTask<IMapObjectReference> GetReference(Map map, Popup item);

        /// <summary>
        /// Gets the <see cref="IMapObjectReference"/> for the popups.
        /// IMPORTANT! Ensure the <see cref="IMapObjectReference"/> items are disposed when you are done with them.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        ValueTask<List<IMapObjectReference>> GetReferences(Map map, IEnumerable<Popup> items);

        /// <summary>
        /// Remove popups.
        /// IMPORTANT! All <see cref="IMapObjectReference"/> items in the list will also be disposed.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="items">List of <see cref="IMapObjectReference"/> to a Popup</param>
        /// <returns></returns>
        ValueTask Remove(Map map, IEnumerable<IMapObjectReference> items);

        /// <summary>
        /// Remove popups.
        /// IMPORTANT! If you have an <see cref="IMapObjectReference"/> to any of the popups they must also be disposed.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        ValueTask Remove(Map map, IEnumerable<Popup> items);

        /// <summary>
        /// Remove a popup.
        /// IMPORTANT! The <see cref="IMapObjectReference"/> will also be disposed.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="item"><see cref="IMapObjectReference"/> to a Popup</param>
        /// <returns></returns>
        ValueTask Remove(Map map, IMapObjectReference item);

        /// <summary>
        /// Remove a popup.
        /// IMPORTANT! If you have an <see cref="IMapObjectReference"/> to the popup it must also be disposed.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        ValueTask Remove(Map map, Popup item);
    }

    internal class AzPopups(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasPopups
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;

        public async ValueTask<List<IMapObjectReference>> Add(Map map, Popup item, bool getReferences = false)
        {
            return await Add(map, [item], getReferences);
        }

        public async ValueTask<List<IMapObjectReference>> Add(Map map, IEnumerable<Popup> items, bool getReferences = false)
        {
            var module = await moduleTask.Value;
            var results = await module.InvokeAsync<List<MapObjectReference>>(GetJsInteropMethod(), map.MapReference, items?.Cast<object>().ToList());
            return [.. results.Cast<IMapObjectReference>()];
        }

        public async ValueTask<IMapObjectReference?> AddHoverPopup(Map map, ILayer layer, Popup item, IEnumerable<string> placeholders, bool getReference = false)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<MapObjectReference?>(GetJsInteropMethod(), map.MapReference, layer.Id, item, placeholders, getReference);
        }

        public async ValueTask<IMapObjectReference> GetReference(Map map, Popup item)
        {
            var results = await GetReferences(map, [item]);
            return results.First();
        }

        public async ValueTask<List<IMapObjectReference>> GetReferences(Map map, IEnumerable<Popup> items)
        {
            var module = await moduleTask.Value;
            var results = await module.InvokeAsync<List<MapObjectReference>>(GetJsInteropMethod(), map.MapReference, items);
            return [.. results.Cast<IMapObjectReference>()];
        }

        #region Remove

        public async ValueTask Remove(Map map, Popup item)
        {
            await Remove(map, [item]);
        }

        public async ValueTask Remove(Map map, IEnumerable<Popup> items)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync(GetJsInteropMethod(), map.MapReference, items);
        }

        public async ValueTask Remove(Map map, IMapObjectReference item)
        {
            await Remove(map, [item]);
        }

        public async ValueTask Remove(Map map, IEnumerable<IMapObjectReference> items)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync(GetJsInteropMethod(), map.MapReference, items.Select(e => e.JsReference).ToList());
            foreach (var item in items)
                await item.DisposeAsync();
        }

        #endregion

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.Popups.GetJsModuleMethod(name);
    }
}
