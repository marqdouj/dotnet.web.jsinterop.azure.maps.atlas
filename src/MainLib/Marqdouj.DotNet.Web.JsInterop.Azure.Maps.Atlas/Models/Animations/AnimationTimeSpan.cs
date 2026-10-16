namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Animations
{
    /// <summary>
    /// Details about a period time.
    /// </summary>
    public class AnimationTimeSpan
    {
        /// <summary>
        /// The start of the time span. Can be a number representing a date/time, or a number representing an order.
        /// </summary>
        public double Begin { get; set; }

        /// <summary>
        /// The end of the time span. Can be a number representing a date/time, or a number representing an order.
        /// </summary>
        public double End { get; set; }

        /// <summary>
        /// The difference between the begin and end values.
        /// </summary>
        public double Length { get; set; }
    }
}
