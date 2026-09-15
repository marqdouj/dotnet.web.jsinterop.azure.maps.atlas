using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Common;
using Marqdouj.DotNet.Web.JsInterop.GeoJson;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models
{
    internal static class ModelExtensions
    {
        internal static void ValidateReferenceType<T>(this IEnumerable<IMapObjectReference>? references) where T : Enum
        {
            if (references == null)
                return;

            var type = typeof(T);
            var bad = references.FirstOrDefault(e => !Enum.IsDefined(type, e.Type ?? ""));
            if (bad != null)
                throw new Exception($"One or more {nameof(IMapObjectReference)} items in the list is not a valid '{type.Name}'.");
        }

        internal static void EnsureCount(this List<double> items, int min, int? max = null, double addDefault = 0)
        {
            while (items.Count < min)
            {
                items.Add(addDefault);
            }

            //Remove excess values
            if (max != null)
            {
                while (items.Count > max)
                {
                    items.RemoveAt(items.Count - 1);
                }
            }
        }

        internal static void EnsureCount(this List<Position> items, int min, int? max = null)
        {
            while (items.Count < min)
            {
                items.Add(new Position(0, 0));
            }

            //Remove excess values
            if (max != null)
            {
                while (items.Count > max)
                {
                    items.RemoveAt(items.Count - 1);
                }
            }
        }
    }
}
