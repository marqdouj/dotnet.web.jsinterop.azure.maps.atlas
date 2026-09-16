using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models;
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
        /// <param name="dataSourceId">id of the datasource to add the features to.</param>
        /// <param name="features"></param>
        /// <returns>List of IDs for the features that were added.</returns>
        ValueTask<List<string>> Add(Map map, string dataSourceId, IEnumerable<object> features);
    }

    internal class AzFeatures(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasFeatures
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;

        public async ValueTask<List<string>> Add(Map map, string dataSourceId, IEnumerable<object> features)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<List<string>>(GetJsInteropMethod(), map.MapReference, dataSourceId, features);
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.Features.GetJsModuleMethod(name);
    }
}
