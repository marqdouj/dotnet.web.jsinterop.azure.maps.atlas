namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Animations
{
    /// <summary>
    /// Defines how the value of a property in two points is extrapolated.
    /// </summary>
    public class PointPairValueInterpolation(string propertyPath, PointInterpolation interpolation = PointInterpolation.Linear)
    {
        /// <summary>
        /// How the interpolation is performed. Certain interpolations require the data to be a certain value.
        /// - `linear`,`min`, `max`, `avg`: `number` or `Date`
        /// - `nearest`: `any`
        /// Default: <see cref="PointInterpolation.Linear"/>
        /// </summary>
        public PointInterpolation Interpolation { get; } = interpolation;

        /// <summary>
        /// The path to the property with each sub-property separated with a forward slash "/",
        /// for example "property/subproperty1/subproperty2".
        /// Array indices can be added as sub-properties as well, for example "property/0".
        /// </summary>
        public string PropertyPath { get; } = propertyPath;
    }
}
