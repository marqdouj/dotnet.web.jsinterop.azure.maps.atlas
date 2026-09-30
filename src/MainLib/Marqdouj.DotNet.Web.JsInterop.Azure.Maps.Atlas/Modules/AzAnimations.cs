using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Animations;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Common;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Sources;
using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules
{
    /// <summary>
    /// Interface for map animations.
    /// </summary>
    public interface IAtlasAnimations
    {
        /// <summary>
        /// Retrieves the name of all the built-in easing functions.
        /// </summary>
        /// <returns></returns>
        ValueTask<List<string>> GetEasingNames();

        /// <summary>
        /// Animates the update of coordinates on a shape. 
        /// Shapes will stay the same type. Only base animation options supported for geometries other than Point. 
        /// </summary>
        /// <param name="map"></param>
        /// <param name="dataSource"><see cref="DataSource"/></param>
        /// <param name="shapeId"></param>
        /// <param name="newCoordinates">The new coordinates of the shape. Must be the same dimension as required by the shape or suitable subset will be picked.</param>
        /// <param name="options"><see cref="PlayableAnimationOptions"/> or <see cref="PathAnimationOptions"/> or <see cref="MapPathAnimationOptions"/></param>
        /// <param name="getReference">If true, an <see cref="IMapObjectReference"/> to the PlayableAnimation will be returned.</param>
        /// <returns><see cref="IMapObjectReference"/> to a PlayableAnimation or null.</returns>
        ValueTask<IMapObjectReference?> SetCoordinates(Map map, DataSource dataSource, string shapeId, object newCoordinates, PlayableAnimationOptions? options, bool getReference = false);
    }

    internal class AzAnimations(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasAnimations
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;

        public async ValueTask<List<string>> GetEasingNames()
        {
            return await moduleTask.InvokeAsyncTC<List<string>>(GetJsInteropMethod());
        }

        public async ValueTask<IMapObjectReference?> SetCoordinates(Map map, DataSource dataSource, string shapeId, object newCoordinates, PlayableAnimationOptions? options, bool getReference = false)
        {
            return await moduleTask.InvokeAsyncTC<MapObjectReference?>(GetJsInteropMethod(), map.MapReference, dataSource, shapeId, newCoordinates, options, getReference);
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.Animations.GetJsModuleMethod(name);
    }
}
