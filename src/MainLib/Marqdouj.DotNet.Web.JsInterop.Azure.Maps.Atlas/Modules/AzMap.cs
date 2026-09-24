using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Configuration;
using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules
{
    /// <summary>
    /// Interface for map interactions.
    /// </summary>
    public interface IAtlasMap
    {
        #region Camera

        /// <summary>
        /// Returns the map camera's current properties.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        ValueTask<MapCamera> GetCamera(Map map);

        /// <summary>
        /// Set the camera of the map control with an animated transition. 
        /// Any options not specified will default to their current values.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="camera"></param>
        /// <param name="cameraBounds"></param>
        /// <param name="animation"></param>
        /// <returns></returns>
        ValueTask SetCamera(Map map, CameraOptions? camera, CameraBoundsOptions? cameraBounds = null, AnimationOptions? animation = null);

        #endregion

        #region Service

        /// <summary>
        /// Get the map service options.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        ValueTask<ServiceOptions> GetServiceOptions(Map map);

        /// <summary>
        /// Set the map service options.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="options"></param>
        /// <param name="setAction"><see cref="SetOptionsAction"/>. Default is <see cref="SetOptionsAction.Update"/> </param>
        /// <returns></returns>
        ValueTask SetServiceOptions(Map map, ServiceOptions options, SetOptionsAction setAction = SetOptionsAction.Update);

        #endregion

        #region Style

        /// <summary>
        /// Get the map style options.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        ValueTask<StyleOptions> GetStyle(Map map);

        /// <summary>
        /// Set the map style options.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="options"></param>
        /// <param name="setAction"><see cref="SetOptionsAction"/>. Default is <see cref="SetOptionsAction.Update"/> </param>
        /// <returns></returns>
        ValueTask SetStyle(Map map, StyleOptions options, SetOptionsAction setAction = SetOptionsAction.Update);

        #endregion

        #region Traffic

        /// <summary>
        /// Get the map traffic options.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        ValueTask<TrafficOptions> GetTraffic(Map map);

        /// <summary>
        /// Set the traffic options for the map.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="options"></param>
        /// <param name="setAction"><see cref="SetOptionsAction"/>. Default is <see cref="SetOptionsAction.Update"/></param>
        /// <returns></returns>
        ValueTask SetTraffic(Map map, TrafficOptions options, SetOptionsAction setAction = SetOptionsAction.Update);

        #endregion

        #region UserInteraction

        /// <summary>
        /// Gets the map user interaction options.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        ValueTask<UserInteractionOptions> GetUserInteraction(Map map);

        /// <summary>
        /// Sets the map user interaction options.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="options"></param>
        /// <param name="setAction"><see cref="SetOptionsAction"/>. Default is <see cref="SetOptionsAction.Update"/></param>
        /// <returns></returns>
        ValueTask SetUserInteraction(Map map, UserInteractionOptions options, SetOptionsAction setAction = SetOptionsAction.Update);

        #endregion

        /// <summary>
        /// Gets the current API version number based on build number.
        /// </summary>
        /// <returns></returns>
        ValueTask<string> GetVersion();

        /// <summary>
        /// Sets the default language used by the map and service modules.
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        ValueTask SetLanguage(string language);

        /// <summary>
        /// Specifies which set of geopolitically disputed borders and labels are displayed on the map.
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        ValueTask SetView(string view);
    }

    internal class AzMap(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasMap
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;

        #region Camera

        public async ValueTask<MapCamera> GetCamera(Map map)
        {
            return await moduleTask.InvokeAsyncTC<MapCamera>(GetJsInteropMethod(), map.MapReference);
        }

        public async ValueTask SetCamera(Map map, CameraOptions? camera, CameraBoundsOptions? cameraBounds = null, AnimationOptions? animation = null)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference, camera, cameraBounds, animation);
        }

        #endregion

        #region Service

        public async ValueTask<ServiceOptions> GetServiceOptions(Map map)
        {
            return await moduleTask.InvokeAsyncTC<ServiceOptions>(GetJsInteropMethod(), map.MapReference);
        }

        public async ValueTask SetServiceOptions(Map map, ServiceOptions options, SetOptionsAction setAction = SetOptionsAction.Update)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference, options, setAction);
        }

        #endregion

        #region Style

        public async ValueTask<StyleOptions> GetStyle(Map map)
        {
            return await moduleTask.InvokeAsyncTC<StyleOptions>(GetJsInteropMethod(), map.MapReference);
        }

        public async ValueTask SetStyle(Map map, StyleOptions options, SetOptionsAction setAction = SetOptionsAction.Update)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference, options, setAction);
        }

        #endregion

        #region Traffic

        public async ValueTask<TrafficOptions> GetTraffic(Map map)
        {
            return await moduleTask.InvokeAsyncTC<TrafficOptions>(GetJsInteropMethod(), map.MapReference);
        }

        public async ValueTask SetTraffic(Map map, TrafficOptions options, SetOptionsAction setAction = SetOptionsAction.Update)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference, options, setAction);
        }

        #endregion

        #region UserInteraction

        public async ValueTask<UserInteractionOptions> GetUserInteraction(Map map)
        {
            return await moduleTask.InvokeAsyncTC<UserInteractionOptions>(GetJsInteropMethod(), map.MapReference);
        }

        public async ValueTask SetUserInteraction(Map map, UserInteractionOptions options, SetOptionsAction setAction = SetOptionsAction.Update)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference, options, setAction);
        }

        #endregion

        #region View

        public async ValueTask<string> GetVersion()
        {
            return await moduleTask.InvokeAsyncTC<string>(GetJsInteropMethod());
        }

        public async ValueTask SetLanguage(string language)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), language);
        }

        public async ValueTask SetView(string view)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), view);
        }

        #endregion

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.Map.GetJsModuleMethod(name);
    }
}
