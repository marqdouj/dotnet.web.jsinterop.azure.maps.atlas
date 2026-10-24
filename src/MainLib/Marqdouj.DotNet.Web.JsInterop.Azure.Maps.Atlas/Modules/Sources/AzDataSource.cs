using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Common;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Configuration;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Sources;
using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules.Sources
{
    /// <summary>
    /// Interface for map DataSource interactions.
    /// </summary>
    public interface IAtlasDataSource
    {
        /// <summary>
        /// Clear the DataSources.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="sources"><see cref="IMapObjectReference"/> to a <see cref="DataSource"/></param>
        /// <returns></returns>
        ValueTask Clear(Map map, IEnumerable<IMapObjectReference> sources);

        /// <summary>
        /// Clear the DataSource.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="source"><see cref="IMapObjectReference"/> to a <see cref="DataSource"/></param>
        /// <returns></returns>
        ValueTask Clear(Map map, IMapObjectReference source);

        /// <summary>
        /// Clear the DataSources.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="sources"><see cref="MapSource"/> for a <see cref="DataSource"/></param>
        /// <returns></returns>
        ValueTask Clear(Map map, IEnumerable<MapSource> sources);

        /// <summary>
        /// Clear the DataSources based on the id.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="ids">DataSource ids</param>
        /// <returns></returns>
        ValueTask Clear(Map map, IEnumerable<string> ids);

        /// <summary>
        /// Clear the DataSource based on the id.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="id">DataSource id</param>
        /// <returns></returns>
        ValueTask Clear(Map map, string id);

        /// <summary>
        /// Gets an <see cref="IMapObjectReference"/> to a Shape.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="source">DataSource.Id or an <see cref="IJSObjectReference"/> to a DataSource.</param>
        /// <param name="shapeId"></param>
        /// <returns><see cref="IMapObjectReference"/> to a Shape or null if not found.</returns>
        ValueTask<IMapObjectReference?> GetShapeById(Map map, object source, string shapeId);

        /// <summary>
        /// Gets a <see cref="IMapObjectReference"/> to a list of all shapes in the DataSource.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="source">DataSource.Id or an <see cref="IJSObjectReference"/> to a DataSource.</param>
        /// <returns><see cref="IMapObjectReference"/> to the shapes.</returns>
        ValueTask<IMapObjectReference> GetShapes(Map map, object source);

        /// <summary>
        /// Downloads a GeoJSON document and imports its data into the data source.
        /// The GeoJSON document must be on the same domain or accessible using CORS.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="source"><see cref="IMapObjectReference"/> for a <see cref="DataSource"/></param>
        /// <param name="url"></param>
        /// <returns></returns>
        ValueTask ImportDataFromUrl(Map map, IMapObjectReference source, string url);

        /// <summary>
        /// Downloads a GeoJSON document and imports its data into the data source.
        /// The GeoJSON document must be on the same domain or accessible using CORS.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="source">Id</param>
        /// <param name="url"></param>
        /// <returns></returns>
        ValueTask ImportDataFromUrl(Map map, string source, string url);

        #region Options

        /// <summary>
        /// Gets the options used by the data source.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        ValueTask<DataSourceOptions?> GetOptions(Map map, DataSource source);

        /// <summary>
        /// Gets the options used by the data source.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        ValueTask<DataSourceOptions?> GetOptions(Map map, string id);

        /// <summary>
        /// Sets the data source options.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="source"></param>
        /// <param name="options"></param>
        /// <param name="action"><see cref="SetOptionsAction"/></param>
        /// <returns></returns>
        ValueTask SetOptions(Map map, DataSource source, DataSourceOptions options, SetOptionsAction action = SetOptionsAction.Update);

        /// <summary>
        /// Sets the data source options.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <param name="action"><see cref="SetOptionsAction"/></param>
        /// <returns></returns>
        ValueTask SetOptions(Map map, string id, DataSourceOptions options, SetOptionsAction action = SetOptionsAction.Update);

        #endregion
    }

    internal class AzDataSource(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasDataSource
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;

        public async ValueTask Clear(Map map, IEnumerable<IMapObjectReference> sources)
        {
            sources.ValidateReferenceType<SourceType>();

            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map, sources.Select(e => e.JsReference));
        }

        public async ValueTask Clear(Map map, IMapObjectReference source)
        {
            await Clear(map, [source]);
        }

        public async ValueTask Clear(Map map, IEnumerable<string> sourceIds)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference, sourceIds);
        }

        public async ValueTask Clear(Map map, IEnumerable<MapSource> sources)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference, sources.Select(e => e.Id));
        }

        public async ValueTask Clear(Map map, string id)
        {
            await Clear(map, [id]);
        }

        public async ValueTask<IMapObjectReference?> GetShapeById(Map map, object source, string shapeId)
        {
            return await moduleTask.InvokeAsyncTC<MapObjectReference?>(GetJsInteropMethod(), map.MapReference, source, shapeId);
        }

        public async ValueTask<IMapObjectReference> GetShapes(Map map, object source)
        {
            return await moduleTask.InvokeAsyncTC<MapObjectReference>(GetJsInteropMethod(), map.MapReference, source);
        }

        public async ValueTask ImportDataFromUrl(Map map, IMapObjectReference source, string url)
        {
            source.ValidateReferenceType<SourceType>(SourceType.Data);

            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference, source.JsReference, url);
        }

        public async ValueTask ImportDataFromUrl(Map map, string source, string url)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference, source, url);
        }

        #region Options

        public async ValueTask<DataSourceOptions?> GetOptions(Map map, DataSource source)
        {
            return await GetOptions(map, source.Id);
        }

        public async ValueTask<DataSourceOptions?> GetOptions(Map map, string id)
        {
            return await moduleTask.InvokeAsyncTC<DataSourceOptions?>(GetJsInteropMethod(), map.MapReference, id);
        }

        public async ValueTask SetOptions(Map map, DataSource source, DataSourceOptions options, SetOptionsAction action = SetOptionsAction.Update)
        {
            await SetOptions(map, source.Id, options, action);
        }

        public async ValueTask SetOptions(Map map, string id, DataSourceOptions options, SetOptionsAction action = SetOptionsAction.Update)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference, id, options, action);
        }

        #endregion

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.DataSource.GetJsModuleMethod(name);
    }
}
