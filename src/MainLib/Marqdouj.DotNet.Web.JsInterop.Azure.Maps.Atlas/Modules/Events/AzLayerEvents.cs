using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Definitions;
using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules.Events
{
    /// <summary>
    /// Interface for layer events.
    /// </summary>
    public interface IAtlasLayerEvents
    {
        /// <summary>
        /// Add layer events.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dotNetRef"></param>
        /// <param name="map"></param>
        /// <param name="events"></param>
        /// <returns></returns>
        ValueTask Add<T>(DotNetObjectReference<T> dotNetRef, Map map, IEnumerable<LayerEvent> events) where T : class;

        /// <summary>
        /// Remove layer events.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="events"></param>
        /// <returns></returns>
        ValueTask Remove(Map map, IEnumerable<LayerEvent> events);
    }

    internal class AzLayerEvents(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasLayerEvents
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;

        public async ValueTask Add<T>(DotNetObjectReference<T> dotNetRef, Map map, IEnumerable<LayerEvent> events) where T : class
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), dotNetRef, map.MapReference, events.Cast<object>().ToList());
        }

        public async ValueTask Remove(Map map, IEnumerable<LayerEvent> events)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference, events.Cast<object>().ToList());
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.LayerEvents.GetJsModuleMethod(name);
    }
}
