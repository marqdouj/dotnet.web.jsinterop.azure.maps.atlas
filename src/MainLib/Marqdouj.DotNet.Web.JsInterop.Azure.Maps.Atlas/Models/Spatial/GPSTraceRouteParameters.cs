using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Animations;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Definitions;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Spatial.Read;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Spatial
{
    /// <summary>
    /// Parameters for loading a GPS trace route.
    /// </summary>
    /// <param name="routeSourceId"><see cref="GPSTraceRouteParameters.RouteSourceId"/></param>
    /// <param name="pinSourceId"><see cref="GPSTraceRouteParameters.PinSourceId"/></param>
    /// <param name="spatialData"><see cref="GPSTraceRouteParameters.SpatialData"/></param>
    /// <param name="timestampProperty"><see cref="GPSTraceRouteParameters.TimestampProperty"/></param>
    /// <param name="readOptions"><see cref="GPSTraceRouteParameters.ReadOptions"/></param>
    /// <param name="pathOptions"><see cref="GPSTraceRouteParameters.PathOptions"/></param>
    /// <param name="events"><see cref="GPSTraceRouteParameters.Events"/></param>
    /// <param name="padding"><see cref="GPSTraceRouteParameters.Padding"/></param>
    public class GPSTraceRouteParameters(
        string routeSourceId,
        string pinSourceId,
        string spatialData,
        string? timestampProperty,
        SpatialDataReadOptions? readOptions,
        RoutePathAnimationOptions? pathOptions,
        List<AnimationEvent>? events,
        int padding = 50)
    {
        /// <summary>
        /// Id for the DataSource that contains the route data.
        /// </summary>
        public string RouteSourceId { get; } = routeSourceId;

        /// <summary>
        /// Id for the DataSource that contains the pin shape.
        /// </summary>
        public string PinSourceId { get; } = pinSourceId;

        /// <summary>
        /// Spatial data string or a URL to a file or zipped file that can be parsed into GeoJSON objects.
        /// </summary>
        public string SpatialData { get; } = spatialData;

        /// <summary>
        /// The route features property name that contains timestamp information.  If not specified, `_timestamp` will be used.
        /// </summary>
        public string? TimestampProperty { get; } = timestampProperty;

        /// <summary>
        /// <inheritdoc cref="SpatialDataReadOptions"/>
        /// </summary>
        public SpatialDataReadOptions? ReadOptions { get; } = readOptions;

        /// <summary>
        /// <inheritdoc cref="RoutePathAnimationOptions"/>
        /// </summary>
        public RoutePathAnimationOptions? PathOptions { get; } = pathOptions;

        /// <summary>
        /// Events for the animation.
        /// </summary>
        public List<AnimationEvent>? Events { get; } = events;

        /// <summary>
        /// The padding to use for <see cref="ZoomToBBox"/>. Can be a number or a <see cref="Padding"/>.
        /// </summary>
        public object? Padding { get; } = padding;

        /// <summary>
        /// If true, zooms the map to the bounding box of the loaded data (if it exists).
        /// </summary>
        public bool ZoomToBBox { get; set; } = true;
    }
}
