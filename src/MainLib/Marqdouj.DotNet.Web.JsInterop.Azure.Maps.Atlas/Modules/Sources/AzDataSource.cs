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
        /// Downloads a GeoJSON document and imports its data into the data id.
        /// The GeoJSON document must be on the same domain or accessible using CORS.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="source"><see cref="IMapObjectReference"/> for a <see cref="DataSource"/></param>
        /// <param name="url"></param>
        /// <param name="zoomToBBox">If true, zooms the map to the bounding box of of all shapes in the datasource.</param>
        /// <param name="padding">The padding to use for <paramref name="zoomToBBox"/>. Can be a number or a <see cref="Padding"/>. Default is 50.</param>
        /// <returns>The ID of the *last* imported shape. Useful when adding GeoJSON that gets imported to the datasource as a single shape, i.e. LineString/MultiLineString etc.</returns>
        ValueTask<string> ImportDataFromUrl(Map map, IMapObjectReference source, string url, bool zoomToBBox = false, object? padding = null);

        /// <summary>
        /// Downloads a GeoJSON document and imports its data into the data id.
        /// The GeoJSON document must be on the same domain or accessible using CORS.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="source">Id</param>
        /// <param name="url"></param>
        /// <param name="zoomToBBox">If true, zooms the map to the bounding box of of all shapes in the datasource.</param>
        /// <param name="padding">The padding to use for <paramref name="zoomToBBox"/>. Can be a number or a <see cref="Padding"/>. Default is 50.</param>
        /// <returns>The ID of the *last* imported shape. Useful when adding GeoJSON that gets imported to the datasource as a single shape, i.e. LineString/MultiLineString etc.</returns>
        ValueTask<string> ImportDataFromUrl(Map map, string source, string url, bool zoomToBBox = false, object? padding = null);

        #region Options

        /// <summary>
        /// Gets the options used by the data id.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        ValueTask<DataSourceOptions?> GetOptions(Map map, DataSource source);

        /// <summary>
        /// Gets the options used by the data id.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        ValueTask<DataSourceOptions?> GetOptions(Map map, string id);

        /// <summary>
        /// Sets the data id options.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="source"></param>
        /// <param name="options"></param>
        /// <param name="action"><see cref="SetOptionsAction"/></param>
        /// <returns></returns>
        ValueTask SetOptions(Map map, DataSource source, DataSourceOptions options, SetOptionsAction action = SetOptionsAction.Update);

        /// <summary>
        /// Sets the data id options.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <param name="action"><see cref="SetOptionsAction"/></param>
        /// <returns></returns>
        ValueTask SetOptions(Map map, string id, DataSourceOptions options, SetOptionsAction action = SetOptionsAction.Update);

        #endregion

        /// <summary>
        /// Exports the datasource to a feature collection and then saves it as JSON.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="source"><see cref="DataSource"/>.</param>
        /// <param name="filename">Valid filename and extension: i.e. 'myDataSource.json'</param>
        /// <returns></returns>
        ValueTask ToJson(Map map, DataSource source, string filename);

        /// <summary>
        /// Exports the datasource to a feature collection and then saves it as JSON.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="id">Id for a <see cref="DataSource"/>.</param>
        /// <param name="filename">Valid filename and extension: i.e. 'myDataSource.json'</param>
        /// <returns></returns>
        ValueTask ToJson(Map map, string id, string filename);

        /// <summary>
        /// Exports the datasource to a feature collection and then returns it as a JSON string.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="source"><see cref="DataSource"/>.</param>
        /// <returns></returns>
        ValueTask<string?> ToJsonString(Map map, DataSource source);

        /// <summary>
        /// Exports the datasource to a feature collection and then returns it as a JSON string.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="id">Id for a <see cref="DataSource"/>.</param>
        /// <returns></returns>
        ValueTask<string?> ToJsonString(Map map, string id);

        /// <summary>
        /// Zooms the map to the bounding box of of all shapes in the datasource.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="source">Id</param>
        /// <param name="padding">Can be a number or a <see cref="Padding"/>. Default is 50.</param>
        /// <returns></returns>
        ValueTask ZoomToBBox(Map map, string source, object? padding = null);

        /// <summary>
        /// Zooms the map to the bounding box of of all shapes in the datasource.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="source"></param>
        /// <param name="padding">Can be a number or a <see cref="Padding"/>. Default is 50.</param>
        /// <returns></returns>
        ValueTask ZoomToBBox(Map map, DataSource source, object? padding = null);

        /// <summary>
        /// Zooms the map to the bounding box of of all shapes found in the datasource, based on shapeId.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="source"></param>
        /// <param name="shapeIds"></param>
        /// <param name="padding">Can be a number or a <see cref="Padding"/>. Default is 50.</param>
        /// <returns></returns>
        ValueTask ZoomToBBox(Map map, DataSource source, IEnumerable<string> shapeIds, object? padding = null);
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
            return await moduleTask.InvokeAsyncTC<IMapObjectReference>(GetJsInteropMethod(), map.MapReference, source);
        }

        public async ValueTask<string> ImportDataFromUrl(Map map, IMapObjectReference source, string url, bool zoomToBBox = false, object? padding = null)
        {
            source.ValidateReferenceType<SourceType>(SourceType.Data);

            return await moduleTask.InvokeAsyncTC<string>(GetJsInteropMethod(), map.MapReference, source.JsReference, url, zoomToBBox, padding);
        }

        public async ValueTask<string> ImportDataFromUrl(Map map, string source, string url, bool zoomToBBox = false, object? padding = null)
        {
            return await moduleTask.InvokeAsyncTC<string>(GetJsInteropMethod(), map.MapReference, source, url, zoomToBBox, padding);
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

        public async ValueTask ToJson(Map map, DataSource source, string filename)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference, source, filename);
        }

        public async ValueTask ToJson(Map map, string id, string filename)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference, id, filename);
        }

        public async ValueTask<string?> ToJsonString(Map map, DataSource source)
        {
            return await moduleTask.InvokeAsyncTC<string?>(GetJsInteropMethod(), map.MapReference, source);
        }

        public async ValueTask<string?> ToJsonString(Map map, string id)
        {
            return await moduleTask.InvokeAsyncTC<string?>(GetJsInteropMethod(), map.MapReference, id);
        }

        public async ValueTask ZoomToBBox(Map map, string source, object? padding = null)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference, source, padding);
        }

        public async ValueTask ZoomToBBox(Map map, DataSource source, object? padding = null)
        {
            await ZoomToBBox(map, source.Id, padding);
        }

        public async ValueTask ZoomToBBox(Map map, DataSource source, IEnumerable<string> shapeIds, object? padding = null)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference, source.Id, padding, shapeIds);
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.DataSource.GetJsModuleMethod(name);
    }
}
