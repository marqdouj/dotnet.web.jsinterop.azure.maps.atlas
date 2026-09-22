using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Configuration;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Controls;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Definitions;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules
{
    /// <summary>
    /// Interface for map create/remove interactions.
    /// </summary>
    public interface IAtlasFactory
    {
        /// <summary>
        /// Creates a new atlas.Map instance.
        /// </summary>
        /// <typeparam name="T">Recommend <see cref="ComponentBase"/>. Blazor will automatically perform the cleanup for JSInvokable methods on <see cref="ComponentBase"/>.</typeparam>
        /// <param name="dotNetRef">
        /// <see cref="DotNetObjectReference{TValue}"/> that has the JSInvokable method for the map NotifyMapEvent'.
        /// <code>
        /// //In JS:
        /// const args: NotifyMapEventArgs = { mapId: mapId, target: 'map', name: 'ready', payload: { [key: string]: any } }
        /// dotNetRef.invokeMethodAsync('NotifyMapEvent', args);
        /// //In c#
        /// [JSInvokable]
        /// public async Task NotifyMapEvent(NotifyMapEventArgs e) => etc...
        /// </code>
        /// </param>
        /// <param name="mapId">/>The id of the html element where the map should be displayed.</param>
        /// <param name="config"><see cref="MapConfiguration"/></param>
        /// <param name="controls">Controls to add to the map (optional).</param>
        /// <param name="events">Events to add to the map. <see cref="MapEvent"/></param>
        /// <returns></returns>
        ValueTask<Map> CreateMap<T>(DotNetObjectReference<T> dotNetRef, string mapId, MapConfiguration config, IEnumerable<MapControl>? controls = null, IEnumerable<MapEvent>? events = null) where T : class;

        /// <summary>
        /// Set the <see cref="LogLevel"/> for this library when logging to the browser console. Default is <see cref="LogLevel.Information"/>.
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        ValueTask SetLogLevel(LogLevel logLevel);

        /// <summary>
        /// Removes and disposes the atlas.Map and the Map instance.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        ValueTask RemoveMap(Map map);
    }

    internal class AzFactory(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasFactory
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;

        public async ValueTask<Map> CreateMap<T>(DotNetObjectReference<T> dotNetRef, string mapId, MapConfiguration configuration, IEnumerable<MapControl>? controls = null, IEnumerable<MapEvent>? events = null) where T : class
        {

            if (configuration.JsLogLevel != null)
                await SetLogLevel((LogLevel)configuration.JsLogLevel);

            try
            {
                var mapRef = await moduleTask.InvokeAsyncInternal<IJSObjectReference>(GetJsInteropMethod(), dotNetRef, mapId, configuration, controls?.Cast<object>().ToList(), events?.Cast<object>().ToList())
                    ?? throw new Exception($"Failed to create an atlas.Map instance where mapId = '{mapId}'.");
                return new Map(mapRef, mapId);
            }
            catch (JSException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async ValueTask SetLogLevel(LogLevel logLevel)
        {
            try
            {
                await moduleTask.InvokeVoidAsyncInternal(GetJsInteropMethod(), logLevel);
            }
            catch (JSException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async ValueTask RemoveMap(Map map)
        {
            try
            {
                await moduleTask.InvokeVoidAsyncInternal(GetJsInteropMethod(), map.MapReference);
                await map.DisposeAsync();
            }
            catch (JSException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.Factory.GetJsModuleMethod(name);
    }
}
