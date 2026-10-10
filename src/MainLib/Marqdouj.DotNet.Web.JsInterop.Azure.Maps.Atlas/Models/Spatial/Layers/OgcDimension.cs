namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Spatial.Layers
{
    /// <summary>
    /// A custom OGC dimension.
    /// </summary>
    public class OgcDimension
    {
        /// <summary>
        /// The identifier of the dimension.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// The default value of the dimension.
        /// </summary>
        public string? Default { get; set; }

        /// <summary>
        /// The value of the dimension. Often indicates the range.
        /// </summary>
        public string? Value { get; set; }
    }
}
