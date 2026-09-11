namespace Marqdouj.DotNet.JsInterop.AzureMaps.Models.Math
{
    /// <summary>
    /// The properties object returned by
    /// <see cref="IAtlasMath.GetPointWithHeadingAlongPath(object, double, DistanceUnits?)"/>
    /// </summary>
    public class HeadingProperties
    {
        /// <summary>
        /// A heading/bearing angle in degrees (0 = North, 90 = East, ...),
        /// pointing in the direction of travel along the path.
        /// </summary>
        public double Heading { get; set; }
    }
}
