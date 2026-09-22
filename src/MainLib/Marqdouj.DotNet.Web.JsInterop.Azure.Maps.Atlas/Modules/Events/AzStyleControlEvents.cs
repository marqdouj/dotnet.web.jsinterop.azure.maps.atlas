using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Definitions;
using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules.Events
{
    /// <summary>
    /// Interface for StyleControl events.
    /// </summary>
    public interface IAtlasStyleControlEvents
    {
        /// <summary>
        /// Add StyleControl events.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dotNetRef"></param>
        /// <param name="map"></param>
        /// <param name="events"></param>
        /// <returns></returns>
        ValueTask Add<T>(DotNetObjectReference<T> dotNetRef, Map map, IEnumerable<StyleControlEvent> events) where T : class;

        /// <summary>
        /// Remove StyleControl events.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="events"></param>
        /// <returns></returns>
        ValueTask Remove(Map map, IEnumerable<StyleControlEvent> events);

        /// <summary>
        /// Remove StyleControl events.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="sources">List of id or <see cref="IJSObjectReference"/> to a StyleControl.</param>
        /// <returns></returns>
        ValueTask RemoveBySource(Map map, IEnumerable<object> sources);
    }

    internal class AzStyleControlEvents(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasStyleControlEvents
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;

        public async ValueTask Add<T>(DotNetObjectReference<T> dotNetRef, Map map, IEnumerable<StyleControlEvent> events) where T : class
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), dotNetRef, map.MapReference, events.Cast<object>().ToList());
        }

        public async ValueTask Remove(Map map, IEnumerable<StyleControlEvent> events)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference, events.Cast<object>().ToList());
        }

        public async ValueTask RemoveBySource(Map map, IEnumerable<object> sources)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference, sources);
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.StyleControlEvents.GetJsModuleMethod(name);
    }
}
