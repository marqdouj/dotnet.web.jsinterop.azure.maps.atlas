using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Definitions;
using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules.Events
{
    /// <summary>
    /// Interface for marker events.
    /// </summary>
    public interface IAtlasMarkerEvents
    {
        /// <summary>
        /// Add marker events.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dotNetRef"></param>
        /// <param name="map"></param>
        /// <param name="events"></param>
        /// <returns></returns>
        ValueTask Add<T>(DotNetObjectReference<T> dotNetRef, Map map, IEnumerable<MarkerEvent> events) where T : class;

        /// <summary>
        /// Remove marker events.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="events"></param>
        /// <returns></returns>
        ValueTask Remove(Map map, IEnumerable<MarkerEvent> events);
    }

    internal class AzMarkerEvents(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasMarkerEvents
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;

        public async ValueTask Add<T>(DotNetObjectReference<T> dotNetRef, Map map, IEnumerable<MarkerEvent> events) where T : class
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), dotNetRef, map.MapReference, events.Cast<object>().ToList());
        }

        public async ValueTask Remove(Map map, IEnumerable<MarkerEvent> events)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference, events.Cast<object>().ToList());
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.MarkerEvents.GetJsModuleMethod(name);
    }
}
