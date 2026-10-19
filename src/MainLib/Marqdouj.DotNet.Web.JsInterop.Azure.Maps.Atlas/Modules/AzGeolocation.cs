using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Definitions;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Payloads;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Geolocation;
using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules
{
    /// <summary>
    /// Interface for Azure Maps Geolocation JavaScript interop methods.
    /// </summary>
    public interface IAtlasGeolocation
    {
        /// <summary>
        /// Clears the watch on the geolocation.
        /// </summary>
        /// <returns></returns>
        ValueTask ClearWatch(int watchId);

        /// <summary>
        /// Gets the current geolocation of the user.
        /// </summary>
        /// <param name="options">The options for retrieving the geolocation.</param>
        /// <returns>The current geolocation of the user.</returns>
        ValueTask<GeolocationPayload> GetLocation(GeolocationPositionOptions? options);

        /// <summary>
        /// Watches the geolocation of the user.
        /// </summary>
        /// <param name="dotNetRef">Handles the watch events. <see cref="DotNetObjectReference{TValue}"/>.</param>
        /// <param name="options">The options for watching the geolocation.</param>
        /// <param name="eventName"><see cref="Event.EventName"/></param>
        /// <returns>The Id of the watch operation; null if the watch was not added.</returns>
        ValueTask<int?> WatchPosition(object dotNetRef, GeolocationPositionOptions? options = null, string? eventName = null);
    }

    internal class AzGeolocation(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasGeolocation
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;

        public async ValueTask<GeolocationPayload> GetLocation(GeolocationPositionOptions? options)
        {
            return await moduleTask.InvokeAsyncTC<GeolocationPayload>(GetJsInteropMethod(), options);
        }

        public async ValueTask<int?> WatchPosition(object dotNetRef, GeolocationPositionOptions? options = null, string? eventName = null)
        {
            return await moduleTask.InvokeAsyncTC<int?>(GetJsInteropMethod(), dotNetRef, options, eventName);
        }

        public async ValueTask ClearWatch(int watchId)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), watchId);
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.Geolocation.GetJsModuleMethod(name);
    }
}