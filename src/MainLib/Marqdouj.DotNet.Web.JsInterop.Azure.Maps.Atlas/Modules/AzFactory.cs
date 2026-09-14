using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Configuration;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Controls;
using Microsoft.AspNetCore.Components;
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
        /// const args: MapEventArgs = { mapId: mapId, target: 'map', name: 'ready', payload: { [key: string]: any } }
        /// dotNetRef.invokeMethodAsync('NotifyMapEvent', args);
        /// //In c#
        /// [JSInvokable]
        /// public async Task NotifyMapEventReady(MapEventArgs e) => etc...
        /// </code>
        /// </param>
        /// <param name="mapId">/>The id of the html element where the map should be displayed.</param>
        /// <param name="config"><see cref="MapConfiguration"/></param>
        /// <param name="controls">Controls to add to the map (optional).</param>
        /// <returns></returns>
        ValueTask<Map> CreateMap<T>(DotNetObjectReference<T> dotNetRef, string mapId, MapConfiguration config, IEnumerable<MapControl>? controls = null) where T : class;
        
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

        public async ValueTask<Map> CreateMap<T>(DotNetObjectReference<T> dotNetRef, string mapId, MapConfiguration configuration, IEnumerable<MapControl>? controls = null) where T : class
        {
            var module = await moduleTask.Value;
            var mapRef = await module.InvokeAsync<IJSObjectReference>(GetJsInteropMethod(), dotNetRef, mapId, configuration, controls?.Cast<object>())
                ?? throw new Exception($"Failed to create an atlas.Map instance where mapId = '{mapId}'.");

            return new Map(mapRef, mapId);
        }

        public async ValueTask RemoveMap(Map map)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync(GetJsInteropMethod(), map.MapReference);
            await map.DisposeAsync();
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.Factory.GetJsModuleMethod(name);
    }
}
