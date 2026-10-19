using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Animations;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Common;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Definitions;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Sources;
using Marqdouj.DotNet.Web.JsInterop.GeoJson;
using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules.Animations
{
    /// <summary>
    /// Interface for map animations.
    /// </summary>
    public interface IAtlasAnimations
    {
        /// <summary>
        /// <inheritdoc cref="IAtlasPlayableAnimation"/>
        /// </summary>
        IAtlasPlayableAnimation PlayableAnimation { get; }

        /// <summary>
        /// <inheritdoc cref="IAtlasRoutePathAnimation"/>
        /// </summary>
        IAtlasRoutePathAnimation RoutePathAnimation { get; }

        /// <summary>
        /// Extracts points from a shapes or features that form a time based route and sorts them by time.
        /// Timestamps must parsable by the `atlas.math.parseTimestamp` function.
        /// All extracted points will have a `_timestamp` property that contains the Date.getTime() value.
        /// Features must be a Point, MultiPoint, or LineString and must contain properties that include timestamp information.
        /// If a timestamp property name is not specified, `_timestamp` will be used.
        /// If a feature collection is passed in, the first shape with a matching timestamp property will dictate what is extracted.
        /// If the first shape is a Point, all points in the colleciton with the timestamp property will be extracted.
        /// If the first shape is a LineString or MultiLineString:
        /// - If it contains a timestamp property with an array of values the same length as coordinates in the feature, new Point features will be created from a combination of the coordinates and timestamp values.
        /// - If the feature has a `waypoints` property that contains an array of Point features with the timestamp property and the same number of coordinates, then these p will be extracted.
        /// </summary>
        /// <param name="shapes">The shapes to extract the route points from.</param>
        /// <param name="timestampProperty">The name of the property that contains the timestamp for each feature. If not specified, defaults to `_timestamp`.</param>
        /// <param name="getReference">If true, returns an <see cref="IJSObjectReference"/> to the data.</param>
        /// <returns>Feature{Point, any}[] or <see cref="IJSObjectReference"/> to the data.</returns>
        ValueTask<object> ExtractRoutePoints(IJSObjectReference shapes, string? timestampProperty, bool getReference = true);

        /// <summary>
        /// Retrieves the name of all the built-in easing functions.
        /// </summary>
        /// <returns></returns>
        ValueTask <List<string>> GetEasingNames();

        /// <summary>
        /// Animates a map and/or a Point shape along a route path. 
        /// The movement will vary based on timestamps within the point feature properties. 
        /// All points must have a `timestamp` property that is a `Date.getTime()` value. 
        /// Use the `extractRoutePoints` function to preprocess data.
        /// </summary>
        /// <param name="dotNetRef"></param>
        /// <param name="map"></param>
        /// <param name="route">Feature{Point, any}[] or its <see cref="IJSObjectReference"/>.</param>
        /// <param name="shape"><see cref="IJSObjectReference"/> to a Shape or HTMLMarker.</param>
        /// <param name="options"><see cref="RoutePathAnimationOptions"/></param>
        /// <param name="events"><see cref="AnimationEvent"/></param>
        /// <returns><see cref="IJSObjectReference"/> to a RoutePathAnimation</returns>
        ValueTask<IJSObjectReference> MoveAlongRoute<T>(DotNetObjectReference<T> dotNetRef, Map map, object route, object? shape = null, RoutePathAnimationOptions? options = null, IEnumerable<AnimationEvent>? events = null) where T : class;

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

        public IAtlasPlayableAnimation  PlayableAnimation { get; } = new AzPlayableAnimation(moduleTask);
        public IAtlasRoutePathAnimation RoutePathAnimation { get; } = new AzRoutePathAnimation(moduleTask);

        public async ValueTask<object> ExtractRoutePoints(IJSObjectReference shapes, string? timestampProperty, bool getReference = true)
        {
            return getReference 
                ? await moduleTask.InvokeAsyncTC<IJSObjectReference>(GetJsInteropMethod(), shapes, timestampProperty, getReference)
                : await moduleTask.InvokeAsyncTC<List<Feature<Point, object?>[]>>(GetJsInteropMethod(), shapes, timestampProperty, getReference);
        }

        public async ValueTask<List<string>> GetEasingNames()
        {
            return await moduleTask.InvokeAsyncTC<List<string>>(GetJsInteropMethod());
        }

        public async ValueTask<IJSObjectReference> MoveAlongRoute<T>(DotNetObjectReference<T> dotNetRef, Map map, object route, object? shape = null, RoutePathAnimationOptions? options = null, IEnumerable<AnimationEvent>? events = null) where T: class
        {
            return await moduleTask.InvokeAsyncTC<IJSObjectReference>(GetJsInteropMethod(), dotNetRef, map.MapReference, route, shape, options, events);
        }

        public async ValueTask<IMapObjectReference?> SetCoordinates(Map map, DataSource dataSource, string shapeId, object newCoordinates, PlayableAnimationOptions? options, bool getReference = false)
        {
            return await moduleTask.InvokeAsyncTC<MapObjectReference?>(GetJsInteropMethod(), map.MapReference, dataSource, shapeId, newCoordinates, options, getReference);
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.Animations.GetJsModuleMethod(name);
    }
}
