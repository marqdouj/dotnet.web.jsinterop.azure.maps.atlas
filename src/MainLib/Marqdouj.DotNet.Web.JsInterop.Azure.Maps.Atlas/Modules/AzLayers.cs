using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Common;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Layers;
using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules
{
    /// <summary>
    /// Interface for map layer interactions.
    /// </summary>
    public interface IAtlasLayers
    {
        /// <summary>
        /// Add map layers.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="layers"></param>
        /// <param name="getReferences">If true, then return the <see cref="IMapObjectReference"/> for the layers. Default is false.</param>
        /// <returns></returns>
        ValueTask<List<IMapObjectReference>> Add(Map map, IEnumerable<ILayer> layers, bool getReferences = false);

        /// <summary>
        /// Get the <see cref="IMapObjectReference"/> for all the layers.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        ValueTask<List<IMapObjectReference>> GetLayers(Map map);

        /// <summary>
        /// Get the <see cref="IMapObjectReference"/> for the layers based on the id.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="layers"></param>
        /// <returns></returns>
        ValueTask<List<IMapObjectReference>> GetLayers(Map map, IEnumerable<ILayer> layers);

        /// <summary>
        /// Get the <see cref="IMapObjectReference"/> for the layers based on the id.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="layerIds"></param>
        /// <returns></returns>
        ValueTask<List<IMapObjectReference>> GetLayers(Map map, IEnumerable<string> layerIds);

        /// <summary>
        /// Remove the layers from the map.
        /// IMPORTANT! All <see cref="IMapObjectReference"/> items in the list will also be disposed.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="layers">List of <see cref="IMapObjectReference"/> to a layer</param>
        /// <returns></returns>
        ValueTask Remove(Map map, IEnumerable<IMapObjectReference> layers);

        /// <summary>
        /// Remove the layer from the map.
        /// IMPORTANT! The <see cref="IMapObjectReference"/> will also be disposed.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="layer"><see cref="IMapObjectReference"/> to a layer</param>
        /// <returns></returns>
        ValueTask Remove(Map map, IMapObjectReference layer);

        /// <summary>
        /// Remove the layers from the map.
        /// IMPORTANT! If you have an <see cref="IMapObjectReference"/> to the layer it must be disposed.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="layers">List of layer ids.</param>
        /// <returns></returns>
        ValueTask Remove(Map map, IEnumerable<ILayer> layers);

        /// <summary>
        /// Remove the layer from the map.
        /// IMPORTANT! If you have an <see cref="IMapObjectReference"/> to the layer it must be disposed.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="layer">Layers to remove</param>
        /// <returns></returns>
        ValueTask Remove(Map map, ILayer layer);

        /// <summary>
        /// Remove the layers from the map.
        /// IMPORTANT! If you have an <see cref="IMapObjectReference"/> to the layer it must be disposed.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        ValueTask Remove(Map map, IEnumerable<string> ids);

        /// <summary>
        /// Remove the layer from the map.
        /// IMPORTANT! If you have an <see cref="IMapObjectReference"/> to the layer it must be disposed.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="id">layer id</param>
        /// <returns></returns>
        ValueTask Remove(Map map, string id);
    }

    internal class AzLayers(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasLayers
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;

        public async ValueTask<List<IMapObjectReference>> Add(Map map, IEnumerable<ILayer> layers, bool getReferences = false)
        {
            var module = await moduleTask.Value;
            var items = await module.InvokeAsync<List<MapObjectReference>>(GetJsInteropMethod(), map.MapReference, layers.Cast<object>().ToList(), getReferences);
            return [.. items.Cast<IMapObjectReference>()];
        }

        public async ValueTask<List<IMapObjectReference>> GetLayers(Map map)
        {
            var module = await moduleTask.Value;
            var items = await module.InvokeAsync<List<MapObjectReference>>(GetJsInteropMethod(), map.MapReference);
            return [.. items.Cast<IMapObjectReference>()];
        }

        public async ValueTask<List<IMapObjectReference>> GetLayers(Map map, IEnumerable<string> layerIds)
        {
            var module = await moduleTask.Value;
            var items = await module.InvokeAsync<List<MapObjectReference>>(GetJsInteropMethod(), map.MapReference, layerIds);
            return [.. items.Cast<IMapObjectReference>()];
        }

        public async ValueTask<List<IMapObjectReference>> GetLayers(Map map, IEnumerable<ILayer> layers)
        {
            var module = await moduleTask.Value;
            var items = await module.InvokeAsync<List<MapObjectReference>>(GetJsInteropMethod(), map.MapReference, layers.Select(e => e.Id));
            return [.. items.Cast<IMapObjectReference>()];
        }

        public async ValueTask Remove(Map map, ILayer layer)
        {
            await Remove(map, [layer]);
        }

        public async ValueTask Remove(Map map, IEnumerable<IMapObjectReference> layers)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync(GetJsInteropMethod(), map.MapReference, layers.Select(e => e.JsReference));
            await layers.CleanUp();
        }

        public async ValueTask Remove(Map map, IMapObjectReference layer)
        {
            await Remove(map, [layer]);
        }

        public async ValueTask Remove(Map map, IEnumerable<string> sourceIds)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync(GetJsInteropMethod(), map.MapReference, sourceIds);
        }

        public async ValueTask Remove(Map map, string id)
        {
            await Remove(map, [id]);
        }

        public async ValueTask Remove(Map map, IEnumerable<ILayer> layers)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync(GetJsInteropMethod(), map.MapReference, layers.Select(e => e.Id));
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.Layers.GetJsModuleMethod(name);
    }
}
