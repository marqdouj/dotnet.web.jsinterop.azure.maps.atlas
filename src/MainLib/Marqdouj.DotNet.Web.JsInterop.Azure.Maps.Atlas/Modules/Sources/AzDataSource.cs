using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Common;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Sources;
using Microsoft.AspNetCore.Components.Web.Virtualization;
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
        /// Downloads a GeoJSON document and imports its data into the data source.
        /// The GeoJSON document must be on the same domain or accessible using CORS.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="source">Id or <see cref="IMapObjectReference"/> for a <see cref="DataSource"/></param>
        /// <param name="url"></param>
        /// <returns></returns>
        ValueTask ImportDataFromUrl(Map map, object source, string url);
    }

    internal class AzDataSource(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasDataSource
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;

        public async ValueTask ImportDataFromUrl(Map map, object source, string url)
        {
            if (source is IMapObjectReference item)
            {
                await ImportDataFromUrl(map, item, url);
            }
            else
            {
                await ImportDataFromUrl(map, (string)source, url);
            }
        }

        private async ValueTask ImportDataFromUrl(Map map, IMapObjectReference source, string url)
        {
            source.ValidateReferenceType<SourceType>(SourceType.Data);

            var module = await moduleTask.Value;
            await module.InvokeVoidAsync(GetJsInteropMethod(), map.MapReference, source.JsReference, url);
        }

        private async ValueTask ImportDataFromUrl(Map map, string sourceId, string url)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync(GetJsInteropMethod(), map.MapReference, sourceId, url);
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.DataSource.GetJsModuleMethod(name);
    }
}
