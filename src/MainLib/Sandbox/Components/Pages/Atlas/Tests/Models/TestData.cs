using Marqdouj.DotNet.Web.JsInterop.GeoJson;

namespace Sandbox.Components.Pages.Atlas.Tests.Models
{
    internal static class TestData
    {
        public static BoundingBox BBox = new(12.803766, 43.033716, 13.129083, 43.33345);

        public static Polygon Poly = new(new List<Position> {
                new(-122.35, 47.65),
                new(-122.30, 47.65),
                new(-122.30, 47.60),
                new(-122.35, 47.60),
                new(-122.35, 47.65)
            });

        public static void WriteNamesToConsole(Type type)
        {
            // Retrieve all methods (public, non-public, instance, static, declared only in this class)
            var methods = type.GetMethods(
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.Static |
                System.Reflection.BindingFlags.DeclaredOnly
            );

            foreach (var method in methods.OrderBy(m => m.Name))
            {
                Console.WriteLine(method.Name);
            }
        }
    }
}
