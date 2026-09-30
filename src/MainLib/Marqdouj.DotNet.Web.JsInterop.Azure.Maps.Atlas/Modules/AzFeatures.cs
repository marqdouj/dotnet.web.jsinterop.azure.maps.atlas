using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Common;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Sources;
using Marqdouj.DotNet.Web.JsInterop.GeoJson;
using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules
{
    /// <summary>
    /// Interface for map feature interactions.
    /// </summary>
    public interface IAtlasFeatures
    {
        /// <summary>
        /// Adds a list of <see cref="IFeature"/> to the map.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="source">DataSource id, <see cref="DataSource"/>, or <see cref="IJSObjectReference"/> for a DataSource</param>
        /// <param name="features"></param>
        /// <param name="getReferences">
        /// If true, then return the <see cref="IMapObjectReference"/> for the items. Default is false.
        /// IMPORTANT! Ensure the <see cref="IMapObjectReference"/> items are disposed when you are done with them.
        /// </param>
        /// <returns></returns>
        ValueTask<List<IMapObjectReference>> Add(Map map, object source, IEnumerable<object> features, bool getReferences = false);

        /// <summary>
        /// Sets the properties for a shape.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="source">DataSource id, <see cref="DataSource"/>, or <see cref="IJSObjectReference"/> for a DataSource</param>
        /// <param name="shapeId"></param>
        /// <param name="properties"><see cref="GeoJsonProperties"/></param>
        /// <returns></returns>
        ValueTask SetProperties(Map map, object source, string shapeId, GeoJsonProperties properties);
    }

    internal class AzFeatures(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasFeatures
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;

        public async ValueTask<List<IMapObjectReference>> Add(Map map, object source, IEnumerable<object> features, bool getReferences = false)
        {
            try
            {
                var items = await moduleTask.InvokeAsyncTC<List<MapObjectReference>>(GetJsInteropMethod(), map.MapReference, source, features, getReferences);
                return [.. items.Cast<IMapObjectReference>()];
            }
            catch (JSException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async ValueTask SetProperties(Map map, object source, string shapeId, GeoJsonProperties properties)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference, source, shapeId, properties);
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.Features.GetJsModuleMethod(name);
    }
}
