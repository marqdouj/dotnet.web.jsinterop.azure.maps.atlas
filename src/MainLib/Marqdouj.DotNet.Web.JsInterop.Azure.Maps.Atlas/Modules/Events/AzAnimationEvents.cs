using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Definitions;
using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules.Events
{
    /// <summary>
    /// Interface for animation events.
    /// </summary>
    public interface IAtlasAnimationEvents
    {
        /// <summary>
        /// Add animation events.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dotNetRef"></param>
        /// <param name="map"></param>
        /// <param name="target"><see cref="IJSObjectReference"/> to the animation.</param>
        /// <param name="events"></param>
        /// <returns></returns>
        ValueTask Add<T>(DotNetObjectReference<T> dotNetRef, Map map, IJSObjectReference target, IEnumerable<AnimationEvent> events) where T : class;

        /// <summary>
        /// Remove animation events.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="target"><see cref="IJSObjectReference"/> to the animation.</param>
        /// <param name="events"></param>
        /// <returns></returns>
        ValueTask Remove(Map map, IJSObjectReference target, IEnumerable<AnimationEvent> events);
    }

    internal class AzAnimationEvents(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasAnimationEvents
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;

        public async ValueTask Add<T>(DotNetObjectReference<T> dotNetRef, Map map, IJSObjectReference target, IEnumerable<AnimationEvent> events) where T : class
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), dotNetRef, map.MapReference, target, events.Cast<object>().ToList());
        }

        public async ValueTask Remove(Map map, IJSObjectReference target, IEnumerable<AnimationEvent> events)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference, target, events.Cast<object>().ToList());
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.AnimationEvents.GetJsModuleMethod(name);
    }
}
