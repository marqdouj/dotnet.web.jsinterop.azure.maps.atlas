using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Definitions;
using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules.Events
{
    /// <summary>
    /// Interface for map events.
    /// </summary>
    public interface IAtlasMapEvents
    {
        /// <summary>
        /// Add events to the map.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dotNetRef">The same <see cref="DotNetObjectReference{TValue}"/> used to create the map.</param>
        /// <param name="map"></param>
        /// <param name="events">Events to add to the map. <see cref="MapEvent"/></param>
        /// <returns></returns>
        ValueTask Add<T>(DotNetObjectReference<T> dotNetRef, Map map, IEnumerable<MapEvent> events) where T : class;

        /// <summary>
        /// Remove events from the map.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="events">Events to remove from the map. <see cref="MapEvent"/></param>
        /// <returns></returns>
        ValueTask Remove(Map map, IEnumerable<MapEvent> events);
    }

    internal class AzMapEvents(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasMapEvents
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;

        public async ValueTask Add<T>(DotNetObjectReference<T> dotNetRef, Map map, IEnumerable<MapEvent> events) where T : class
        {
            await moduleTask.InvokeVoidAsyncInternal(GetJsInteropMethod(), dotNetRef, map.MapReference, events.Cast<object>().ToList());
        }

        public async ValueTask Remove(Map map, IEnumerable<MapEvent> events)
        {
            await moduleTask.InvokeVoidAsyncInternal(GetJsInteropMethod(), map.MapReference, events.Cast<object>().ToList());
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.MapEvents.GetJsModuleMethod(name);
    }
}
