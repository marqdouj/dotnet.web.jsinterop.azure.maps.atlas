using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Common;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Sources;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules.Sources
{
    /// <summary>
    /// Interface for map source interactions.
    /// </summary>
    public interface IAtlasSources
    {
        /// <summary>
        /// <inheritdoc cref="IAtlasDataSource"/>
        /// </summary>
        IAtlasDataSource DataSource { get; }

        /// <summary>
        /// Add map sources.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="sources"></param>
        /// <param name="getReferences">
        /// If true, then return the <see cref="IMapObjectReference"/> for the sources. Default is false.
        /// IMPORTANT! Ensure the <see cref="IMapObjectReference"/> items are disposed when you are done with them.
        /// </param>
        /// <returns></returns>
        ValueTask<List<IMapObjectReference>> Add(Map map, IEnumerable<MapSource> sources, bool getReferences = false);

        /// <summary>
        /// Get the <see cref="IMapObjectReference"/> for all the sources.
        /// IMPORTANT! Ensure the <see cref="IMapObjectReference"/> items are disposed when you are done with them.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        ValueTask<List<IMapObjectReference>> GetSources(Map map);

        /// <summary>
        /// Get the <see cref="IMapObjectReference"/> for the sources based on the id.
        /// IMPORTANT! Ensure the <see cref="IMapObjectReference"/> items are disposed when you are done with them.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="sources"></param>
        /// <returns></returns>
        ValueTask<List<IMapObjectReference>> GetSources(Map map, IEnumerable<MapSource> sources);

        /// <summary>
        /// Get the <see cref="IMapObjectReference"/> for the sources based on the id.
        /// IMPORTANT! Ensure the <see cref="IMapObjectReference"/> items are disposed when you are done with them.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="sourceIds"></param>
        /// <returns></returns>
        ValueTask<List<IMapObjectReference>> GetSources(Map map, IEnumerable<string> sourceIds);

        /// <summary>
        /// Remove the sources from the map.
        /// IMPORTANT! All <see cref="IMapObjectReference"/> items in the list will also be disposed.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="sources">List of <see cref="IMapObjectReference"/> to a <see cref="MapSource"/></param>
        /// <returns></returns>
        ValueTask Remove(Map map, IEnumerable<IMapObjectReference> sources);

        /// <summary>
        /// Remove the sources from the map.
        /// IMPORTANT! The <see cref="IMapObjectReference"/> will also be disposed.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="source"><see cref="IMapObjectReference"/> to a <see cref="MapSource"/></param>
        /// <returns></returns>
        ValueTask Remove(Map map, IMapObjectReference source);

        /// <summary>
        /// Remove the sources from the map based on Id.
        /// IMPORTANT! If you have an <see cref="IMapObjectReference"/> to the source it must be disposed.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="sources"></param>
        /// <returns></returns>
        ValueTask Remove(Map map, IEnumerable<MapSource> sources);

        /// <summary>
        /// Remove the sources from the map based on Id.
        /// IMPORTANT! If you have an <see cref="IMapObjectReference"/> to the source it must be disposed.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        ValueTask Remove(Map map, MapSource source);

        /// <summary>
        /// Remove the sources from the map based on Id.
        /// IMPORTANT! If you have an <see cref="IMapObjectReference"/> to the source it must be disposed.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="sourceIds"></param>
        /// <returns></returns>
        ValueTask Remove(Map map, IEnumerable<string> sourceIds);

        /// <summary>
        /// Remove the sources from the map based on Id.
        /// IMPORTANT! If you have an <see cref="IMapObjectReference"/> to the source it must be disposed.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="sourceId">Id to the <see cref="MapSource"/></param>
        /// <returns></returns>
        ValueTask Remove(Map map, string sourceId);
    }

    internal class AzSources(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasSources
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;

        public IAtlasDataSource DataSource { get; } = new AzDataSource(moduleTask);

        public async ValueTask<List<IMapObjectReference>> Add(Map map, IEnumerable<MapSource> sources, bool getReferences = false)
        {
            var module = await moduleTask.Value;
            var items = await module.InvokeAsync<List<MapObjectReference>>(GetJsInteropMethod(), map.MapReference, sources, getReferences);
            return [.. items.Cast<IMapObjectReference>()];
        }

        public async ValueTask<List<IMapObjectReference>> GetSources(Map map)
        {
            var module = await moduleTask.Value;
            var items = await module.InvokeAsync<List<MapObjectReference>>(GetJsInteropMethod(), map.MapReference);
            return [.. items.Cast<IMapObjectReference>()];
        }

        public async ValueTask<List<IMapObjectReference>> GetSources(Map map, IEnumerable<string> sourceIds)
        {
            var module = await moduleTask.Value;
            var items = await module.InvokeAsync<List<MapObjectReference>>(GetJsInteropMethod(), map.MapReference, sourceIds);
            return [.. items.Cast<IMapObjectReference>()];
        }

        public async ValueTask<List<IMapObjectReference>> GetSources(Map map, IEnumerable<MapSource> sources)
        {
            var module = await moduleTask.Value;
            var items = await module.InvokeAsync<List<MapObjectReference>>(GetJsInteropMethod(), map.MapReference, sources.Select(e => e.Id));
            return [.. items.Cast<IMapObjectReference>()];
        }

        public async ValueTask Remove(Map map, IEnumerable<IMapObjectReference> sources)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync(GetJsInteropMethod(), map.MapReference, sources.Select(e => e.JsReference));
            await sources.DisposeItems();
        }

        public async ValueTask Remove(Map map, IMapObjectReference source)
        {
            await Remove(map, [source]);
        }

        public async ValueTask Remove(Map map, IEnumerable<string> sourceIds)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync(GetJsInteropMethod(), map.MapReference, sourceIds);
        }

        public async ValueTask Remove(Map map, string sourceId)
        {
            await Remove(map, [sourceId]);
        }

        public async ValueTask Remove(Map map, IEnumerable<MapSource> sources)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync(GetJsInteropMethod(), map.MapReference, sources.Select(e => e.Id));
        }

        public async ValueTask Remove(Map map, MapSource source)
        {
            await Remove(map, [source]);
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.Sources.GetJsModuleMethod(name);
    }
}
