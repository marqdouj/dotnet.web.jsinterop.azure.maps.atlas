namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Sources
{
    /// <summary>
    /// Options for configuring an elevation tile source.
    /// Inherits from <see cref="RasterTileSourceOptions"/> and adds an optional Encoding property.
    /// </summary>
    public class ElevationTileSourceOptions : RasterTileSourceOptions
    {
        /// <summary>
        /// <inheritdoc cref="ElevationTileEncoding"/>.
        /// Default is 'mapbox'.
        /// </summary>
        public ElevationTileEncoding? Encoding { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns>MemberwiseClone</returns>
        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
}
