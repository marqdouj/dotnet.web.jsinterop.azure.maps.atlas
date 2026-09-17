using System.Runtime.CompilerServices;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules
{
    internal enum JsModule
    {
        BoundingBox,
        Controls,
        DataSource,
        Factory,
        Features,
        Layers,
        Math,
        Map,
        MapEvents,
        MercatorPoint,
        Position,
        Sources,
    }

    internal static class ModuleExtensions
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
