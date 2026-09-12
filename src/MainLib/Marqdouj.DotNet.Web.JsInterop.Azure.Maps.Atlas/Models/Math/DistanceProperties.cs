using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules;

namespace Marqdouj.DotNet.JsInterop.AzureMaps.Models.Math
{
    /// <summary>
    /// The properties object returned by
    /// <see cref="IAtlasMath.GetClosestPointOnGeometry(object, object, DistanceUnits?, double?)"/>
    /// </summary>
    public class DistanceProperties
    {
        /// <summary>
        /// Specifies the distance between the two points in the specified units.
        /// </summary>
        public double distance { get; set; }
    }
}
