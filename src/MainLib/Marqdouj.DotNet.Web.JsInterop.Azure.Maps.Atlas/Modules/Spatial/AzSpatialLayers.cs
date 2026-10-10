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
    }

    internal class AzSpatialLayers(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasSpatialLayers
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;

        public async ValueTask<List<IMapObjectReference>> Add(Map map, IEnumerable<SpatialLayer> layers, bool getReferences = false)
        {
            var items = await moduleTask.InvokeAsyncTC<List<MapObjectReference>>(GetJsInteropMethod(), map.MapReference, layers.Cast<object>().ToList(), getReferences);
            return [.. items.Cast<IMapObjectReference>()];
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.SpatialLayers.GetJsModuleMethod(name);
    }
}
