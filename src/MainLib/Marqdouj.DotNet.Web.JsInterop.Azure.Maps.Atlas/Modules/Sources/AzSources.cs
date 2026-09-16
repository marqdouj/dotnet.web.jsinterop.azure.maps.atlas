using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Common;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Sources;
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
        /// <param name="getReferences">If true, then return the <see cref="IMapObjectReference"/> for the sources. Default is false.</param>
        /// <returns></returns>
        ValueTask<List<IMapObjectReference>> Add(Map map, IEnumerable<MapSource> sources, bool getReferences = false);

        /// <summary>
        /// Clear the sources.
        /// </summary>
        /// <param name="sources"></param>
        /// <returns></returns>
        ValueTask Clear(IEnumerable<IMapObjectReference> sources);

        /// <summary>
        /// Clear the sources based on the id.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="sources"></param>
        /// <returns></returns>
        ValueTask ClearById(Map map, IEnumerable<MapSource> sources);

        /// <summary>
        /// Clear the sources based on the id.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="sourceIds"></param>
        /// <returns></returns>
        ValueTask ClearById(Map map, IEnumerable<string> sourceIds);

        /// <summary>
        /// Get the <see cref="IMapObjectReference"/> for all the sources.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        ValueTask<List<IMapObjectReference>> GetSources(Map map);

        /// <summary>
        /// Get the <see cref="IMapObjectReference"/> for the sources based on the id.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="sources"></param>
        /// <returns></returns>
        ValueTask<List<IMapObjectReference>> GetSourcesById(Map map, IEnumerable<MapSource> sources);

        /// <summary>
        /// Get the <see cref="IMapObjectReference"/> for the sources based on the id.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="sourceIds"></param>
        /// <returns></returns>
        ValueTask<List<IMapObjectReference>> GetSourcesById(Map map, IEnumerable<string> sourceIds);

        /// <summary>
        /// Remove the sources from the map.
        /// IMPORTANT! All <see cref="IMapObjectReference"/> items in the list will also be disposed.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="sources"></param>
        /// <returns></returns>
        ValueTask Remove(Map map, IEnumerable<IMapObjectReference> sources);

        /// <summary>
        /// Remove the sources from the map based on Id.
        /// IMPORTANT! If you have an <see cref="IMapObjectReference"/> to the source it must be disposed.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="sources"></param>
        /// <returns></returns>
        ValueTask RemoveById(Map map, IEnumerable<MapSource> sources);

        /// <summary>
        /// Remove the sources from the map based on Id.
        /// IMPORTANT! If you have an <see cref="IMapObjectReference"/> to the source it must be disposed.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="sourceIds"></param>
        /// <returns></returns>
        ValueTask RemoveById(Map map, IEnumerable<string> sourceIds);
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

        public async ValueTask Clear(IEnumerable<IMapObjectReference> sources)
        {
            sources.ValidateReferenceType<SourceType>();

            var module = await moduleTask.Value;
            await module.InvokeVoidAsync(GetJsInteropMethod(), sources.Select(e => e.JsReference));
        }

        public async ValueTask ClearById(Map map, IEnumerable<string> sourceIds)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync(GetJsInteropMethod(), map.MapReference, sourceIds);
        }

        public async ValueTask ClearById(Map map, IEnumerable<MapSource> sources)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync(GetJsInteropMethod(), map.MapReference, sources.Select(e => e.Id));
        }

        public async ValueTask<List<IMapObjectReference>> GetSources(Map map)
        {
            var module = await moduleTask.Value;
            var items = await module.InvokeAsync<List<MapObjectReference>>(GetJsInteropMethod(), map.MapReference);
            return [.. items.Cast<IMapObjectReference>()];
        }

        public async ValueTask<List<IMapObjectReference>> GetSourcesById(Map map, IEnumerable<string> sourceIds)
        {
            var module = await moduleTask.Value;
            var items = await module.InvokeAsync<List<MapObjectReference>>(GetJsInteropMethod(), map.MapReference, sourceIds);
            return [.. items.Cast<IMapObjectReference>()];
        }

        public async ValueTask<List<IMapObjectReference>> GetSourcesById(Map map, IEnumerable<MapSource> sources)
        {
            var module = await moduleTask.Value;
            var items = await module.InvokeAsync<List<MapObjectReference>>(GetJsInteropMethod(), map.MapReference, sources.Select(e => e.Id));
            return [.. items.Cast<IMapObjectReference>()];
        }

        public async ValueTask Remove(Map map, IEnumerable<IMapObjectReference> sources)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync(GetJsInteropMethod(), map.MapReference, sources.Select(e => e.JsReference));
            foreach (var item in sources)
                await item.DisposeAsync();
        }

        public async ValueTask RemoveById(Map map, IEnumerable<string> sourceIds)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync(GetJsInteropMethod(), map.MapReference, sourceIds);
        }

        public async ValueTask RemoveById(Map map, IEnumerable<MapSource> sources)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync(GetJsInteropMethod(), map.MapReference, sources.Select(e => e.Id));
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.Sources.GetJsModuleMethod(name);
    }
}
