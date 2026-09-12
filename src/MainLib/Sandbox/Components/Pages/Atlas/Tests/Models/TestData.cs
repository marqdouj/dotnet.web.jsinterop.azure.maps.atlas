using Marqdouj.DotNet.Web.JsInterop.GeoJson;

namespace Sandbox.Components.Pages.Atlas.Tests.Models
{
    internal static class TestData
    {
        public static BoundingBox BBox = new BoundingBox(12.803766, 43.033716, 13.129083, 43.33345);

        public static Polygon Poly = new Polygon(new List<Position> {
                new Position(-122.35, 47.65),
                new Position(-122.30, 47.65),
                new Position(-122.30, 47.60),
                new Position(-122.35, 47.60),
                new Position(-122.35, 47.65)
            });
    }
}
