using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Common;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Spatial.Layers;
using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules.Spatial
{
    /// <summary>
    /// Interface for map spatial layer interactions.
    /// </summary>
    public interface IAtlasSpatialLayers
    {
        /// <summary>
        /// Adds the spatial layers to the map.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="layers"></param>
        /// <param name="getReferences"></param>
        /// <returns></returns>
        ValueTask<List<IMapObjectReference>> Add(Map map, IEnumerable<SpatialLayer> layers, bool getReferences = false);

        /// <summary>
        /// Adds the spatial layer to the map.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="layer"></param>
        /// <param name="getReferences"></param>
        /// <returns></returns>
        ValueTask<IMapObjectReference?> Add(Map map, SpatialLayer layer, bool getReferences = false);

        /// <summary>
        /// Gets the layer options.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="layerType"></param>
        /// <param name="layer"><see cref="SpatialLayer"/>, SpatialLayer.Id, or <see cref="IJSObjectReference"/> to the layer.</param>
        /// <returns></returns>
        ValueTask<object?> GetOptions(Map map, SpatialLayerType layerType, object layer);

        /// <summary>
        /// Sets the layer options.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="layer"><see cref="SpatialLayer"/>, SpatialLayer.Id, or <see cref="IJSObjectReference"/> to the layer.</param>
        /// <param name="options"></param>
        /// <returns></returns>
        ValueTask SetOptions(Map map, object layer, object options);
    }

    internal class AzSpatialLayers(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasSpatialLayers
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;

        public async ValueTask<List<IMapObjectReference>> Add(Map map, IEnumerable<SpatialLayer> layers, bool getReferences = false)
        {
            var items = await moduleTask.InvokeAsyncTC<List<MapObjectReference>>(GetJsInteropMethod(), map.MapReference, layers.Cast<object>().ToList(), getReferences);
            return [.. items.Cast<IMapObjectReference>()];
        }

        public async ValueTask<IMapObjectReference?> Add(Map map, SpatialLayer layer, bool getReferences = false)
        {
            var result = await Add(map, [layer], getReferences);
            return result.FirstOrDefault();
        }

        public async ValueTask<object?> GetOptions(Map map, SpatialLayerType layerType, object layer)
        {
            return layerType switch
            {
                SpatialLayerType.SimpleDataLayer => await moduleTask.InvokeAsyncTC<SimpleDataLayerOptions>(GetJsInteropMethod(), map.MapReference, layer),
                SpatialLayerType.OgcMapLayer => await moduleTask.InvokeAsyncTC<OgcMapLayerOptions>(GetJsInteropMethod(), map.MapReference, layer),
                _ => null,
            };
        }

        public async ValueTask SetOptions(Map map, object layer, object options)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference, layer, options);
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.SpatialLayers.GetJsModuleMethod(name);
    }
}
