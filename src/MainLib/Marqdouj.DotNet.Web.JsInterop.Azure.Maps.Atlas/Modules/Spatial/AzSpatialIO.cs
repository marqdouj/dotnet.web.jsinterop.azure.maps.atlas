using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Configuration;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Sources;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Spatial;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Spatial.Read;
using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules.Spatial
{
    /// <summary>
    /// Interface for map spatial IO interactions.
    /// </summary>
    public interface IAtlasSpatialIO
    {
        /// <summary>
        /// Loads a GPS trace route for animation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dotNetRef"></param>
        /// <param name="map"></param>
        /// <param name="routeParams"></param>
        /// <returns><see cref="GPSTraceRouteResult"/></returns>
        ValueTask<GPSTraceRouteResult> LoadGPSTraceRoute<T>(DotNetObjectReference<T> dotNetRef, Map map, GPSTraceRouteParameters routeParams) where T : class;

        /// <summary>
        /// Reads a spatial data string or a URL to a file or zipped file and parses the spatial data into GeoJSON objects.
        /// Supported spatial data formats: KML, KMZ, GPX, GeoRSS, GML, spatial delimited files (CSV), GeoJSON.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="dataSource">DataSource.Id or an <see cref="IJSObjectReference"/> to a <see cref="DataSource"/></param>
        /// <param name="spatialData">The spatial data string or URL to a file to read.</param>
        /// <param name="options"></param>
        /// <param name="dsReadAction"><see cref="DataSourceReadAction"/></param>
        /// <param name="zoomToBBox">If true, zooms the map to the bounding box of the loaded data (if it exists).</param>
        /// <param name="padding">The padding to use for zoomToBBox. Can be a number or a <see cref="Padding"/>.</param>
        /// <returns></returns>
        ValueTask Read(Map map, object dataSource, string spatialData, SpatialDataReadOptions? options = null, DataSourceReadAction dsReadAction = DataSourceReadAction.add, bool zoomToBBox = true, object? padding = null);
    }

    internal class AzSpatialIO(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasSpatialIO
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;

        public async ValueTask<GPSTraceRouteResult> LoadGPSTraceRoute<T>(DotNetObjectReference<T> dotNetRef, Map map, GPSTraceRouteParameters routeParams) where T: class
        {
            return await moduleTask.InvokeAsyncTC<GPSTraceRouteResult>(GetJsInteropMethod(), dotNetRef, map.MapReference, routeParams);
        }

        public async ValueTask Read(Map map, object dataSource, string spatialData, SpatialDataReadOptions? options = null, DataSourceReadAction dsReadAction = DataSourceReadAction.add, bool zoomToBBox = true, object? padding = null)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference, dataSource, spatialData, options, dsReadAction, zoomToBBox, padding);
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.SpatialIO.GetJsModuleMethod(name);
    }
}
