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
        /// <summary>
        /// Returns the map camera's current properties.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        Task<MapCamera> GetCamera(Map map);

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
    }

    internal class AzMap(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasMap
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;

        public async Task<MapCamera> GetCamera(Map map)
        {
            return await moduleTask.InvokeAsyncTC<MapCamera>(GetJsInteropMethod(), map.MapReference);
        }

        public async ValueTask SetCamera(Map map, CameraOptions? camera, CameraBoundsOptions? cameraBounds = null, AnimationOptions? animation = null)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference, camera, cameraBounds, animation);
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.Map.GetJsModuleMethod(name);
    }
}
