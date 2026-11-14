using System.Runtime.CompilerServices;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules
{
    internal enum JsModule
    {
        Animations,
        AnimationEvents,
        BoundingBox,
        Controls,
        DataSource,
        Factory,
        Features,
        Geolocation,
        ImageSprite,
        LayerEvents,
        Layers,
        MarkerEvents,
        Markers,
        Math,
        Map,
        MapEvents,
        MercatorPoint,
        Navigator,
        PlayableAnimation,
        Popups,
        Position,
        RoutePathAnimation,
        Sources,
        Spatial,
        SpatialLayers,
        SpatialIO,
        StyleControlEvents,
    }

    /// <summary>
    /// 
    /// </summary>
    public static class ModuleExtensions
    {
        internal static string GetJsModuleMethod(this JsModule module, [CallerMemberName] string name = "")
            => $"{module}.{name.ToJsonName()}";

        /// <summary>
        /// first char must be lowercase
        /// </summary>
        internal static string ToJsonName(this string name)
        {
            var firstChar = name[0].ToString().ToLower();
            var remainder = name[1..];
            return $"{firstChar}{remainder}";
        }
    }
}
