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
        /// <param name="getReferences">If true, then return the <see cref="IMapObjectReference"/> for the layers.</param>
        /// <returns></returns>
        ValueTask<List<IMapObjectReference>> Add(Map map, IEnumerable<ILayer> layers, bool getReferences = false);

        /// <summary>
        /// Adds a map layer.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="layer"></param>
        /// <param name="getReference">If true, then return the <see cref="IMapObjectReference"/> for the layer.</param>
        /// <returns></returns>
        ValueTask<IMapObjectReference?> Add(Map map, ILayer layer, bool getReference = false);

        /// <summary>
        /// Removes all layers from the map.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        ValueTask Clear(Map map);

        /// <summary>
        /// Gets the layer options.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="layerType"><see cref="LayerType"/></param>
        /// <param name="layer"><see cref="ILayer"/> or <see cref="IJSObjectReference"/> to a layer.</param>
        /// <returns></returns>
        ValueTask<LayerOptions?> GetOptions(Map map, LayerType layerType, object layer);

        /// <summary>
        /// Get the <see cref="IMapObjectReference"/> for all the layers.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        ValueTask<List<IMapObjectReference>> GetReferences(Map map);

        /// <summary>
        /// Get the <see cref="IMapObjectReference"/> for the layers based on the id.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="layers"></param>
        /// <returns></returns>
        ValueTask<List<IMapObjectReference>> GetReferences(Map map, IEnumerable<ILayer> layers);

        /// <summary>
        /// Get the <see cref="IMapObjectReference"/> for the layers based on the id.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="layerIds"></param>
        /// <returns></returns>
        ValueTask<List<IMapObjectReference>> GetReferences(Map map, IEnumerable<string> layerIds);

        /// <summary>
        /// Moves a layer to a different z-position.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="id">The id of the layer to move.</param>
        /// <param name="before">Optionally specify to move the layer before this layer.</param>
        /// <returns></returns>
        ValueTask Move(Map map, string id, string? before);

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
        /// <param name="layers">List of layer layerIds.</param>
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

        /// <summary>
        /// Sets the layer options.
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="map"></param>
        /// <param name="layer"><see cref="ILayer"/> or <see cref="IJSObjectReference"/> to a layer.</param>
        /// <param name="options"></param>
        /// <returns></returns>
        ValueTask SetOptions<TOptions>(Map map, object layer, TOptions options) where TOptions : LayerOptions;
    }

    internal class AzLayers(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasLayers
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;

        public async ValueTask<List<IMapObjectReference>> Add(Map map, IEnumerable<ILayer> layers, bool getReferences = false)
        {
            var items = await moduleTask.InvokeAsyncTC<List<MapObjectReference>>(GetJsInteropMethod(), map.MapReference, layers.Cast<object>().ToList(), getReferences);
            return [.. items.Cast<IMapObjectReference>()];
        }

        public async ValueTask<IMapObjectReference?> Add(Map map, ILayer layer, bool getReference = false)
        {
            var items = await Add(map, [layer], getReference);
            return items.FirstOrDefault();
        }

        public async ValueTask Clear(Map map)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference);
        }

        public async ValueTask<List<IMapObjectReference>> GetReferences(Map map)
        {
            var items = await moduleTask.InvokeAsyncTC<List<MapObjectReference>>(GetJsInteropMethod(), map.MapReference);
            return [.. items.Cast<IMapObjectReference>()];
        }

        public async ValueTask<List<IMapObjectReference>> GetReferences(Map map, IEnumerable<string> layerIds)
        {
            var items = await moduleTask.InvokeAsyncTC<List<MapObjectReference>>(GetJsInteropMethod(), map.MapReference, layerIds);
            return [.. items.Cast<IMapObjectReference>()];
        }

        public async ValueTask<List<IMapObjectReference>> GetReferences(Map map, IEnumerable<ILayer> layers)
        {
            return  await GetReferences(map, layers.Select(e => e.Id));
        }

        public async ValueTask Move(Map map, string id, string? before)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference, id, before);
        }

        public async ValueTask Remove(Map map, ILayer layer)
        {
            await Remove(map, [layer]);
        }

        public async ValueTask Remove(Map map, IEnumerable<IMapObjectReference> layers)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference, layers.Select(e => e.JsReference));
            await layers.DisposeItems();
        }

        public async ValueTask Remove(Map map, IMapObjectReference layer)
        {
            await Remove(map, [layer]);
        }

        public async ValueTask Remove(Map map, IEnumerable<string> layerIds)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference, layerIds);
        }

        public async ValueTask Remove(Map map, string id)
        {
            await Remove(map, [id]);
        }

        public async ValueTask Remove(Map map, IEnumerable<ILayer> layers)
        {
            await Remove(map, layers.Select(e => e.Id));
        }

        public async ValueTask<LayerOptions?> GetOptions(Map map, LayerType layerType, object layer)
        {
            return layerType switch
            {
                LayerType.Bubble => await moduleTask.InvokeAsyncTC<BubbleLayerOptions>(GetJsInteropMethod(), map.MapReference, layer),
                LayerType.HeatMap => await moduleTask.InvokeAsyncTC<HeatMapLayerOptions>(GetJsInteropMethod(), map.MapReference, layer),
                LayerType.Image => await moduleTask.InvokeAsyncTC<ImageLayerOptions>(GetJsInteropMethod(), map.MapReference, layer),
                LayerType.Line => await moduleTask.InvokeAsyncTC<LineLayerOptions>(GetJsInteropMethod(), map.MapReference, layer),
                LayerType.Polygon => await moduleTask.InvokeAsyncTC<PolygonLayerOptions>(GetJsInteropMethod(), map.MapReference, layer),
                LayerType.PolygonExtrusion => await moduleTask.InvokeAsyncTC<PolygonExtrusionLayerOptions>(GetJsInteropMethod(), map.MapReference, layer),
                LayerType.Symbol => await moduleTask.InvokeAsyncTC<SymbolLayerOptions>(GetJsInteropMethod(), map.MapReference, layer),
                LayerType.Tile => await moduleTask.InvokeAsyncTC<TileLayerOptions>(GetJsInteropMethod(), map.MapReference, layer),
                _ => null,
            };
        }

        public async ValueTask SetOptions<TOptions>(Map map, object layer, TOptions options) where TOptions : LayerOptions
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference, layer, options);
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.Layers.GetJsModuleMethod(name);
    }
}
