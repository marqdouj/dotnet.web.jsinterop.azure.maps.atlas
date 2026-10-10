using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Sources;
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
        /// Reads a spatial data string or a URL to a file or zipped file and parses the spatial data into GeoJSON objects.
        /// Supported spatial data formats: KML, KMZ, GPX, GeoRSS, GML, spatial delimited files (CSV), GeoJSON.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="dataSource">DataSource.Id or an <see cref="IJSObjectReference"/> to a <see cref="DataSource"/></param>
        /// <param name="spatialData">The spatial data string or URL to a file to read.</param>
        /// <param name="options"></param>
        /// <param name="dsReadAction"><see cref="DataSourceReadAction"/></param>
        /// <param name="zoomToBBox">If true, zooms the map to the bounding box of the loaded data (if it exists).</param>
        /// <returns></returns>
        ValueTask Read(Map map, object dataSource, string spatialData, SpatialDataReadOptions? options = null, DataSourceReadAction dsReadAction = DataSourceReadAction.add, bool zoomToBBox = true);
    }

    internal class AzSpatialIO(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasSpatialIO
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;

        public async ValueTask Read(Map map, object dataSource, string spatialData, SpatialDataReadOptions? options = null, DataSourceReadAction dsReadAction = DataSourceReadAction.add, bool zoomToBBox = true)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference, dataSource, spatialData, options, dsReadAction, zoomToBBox);
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.SpatialIO.GetJsModuleMethod(name);
    }
}
