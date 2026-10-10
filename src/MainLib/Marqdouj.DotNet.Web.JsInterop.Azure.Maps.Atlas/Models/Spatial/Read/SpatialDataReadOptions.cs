namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Spatial.Read
{
    /// <summary>
    /// Options that customize how spatial files are read and parsed.
    /// </summary>
    public class SpatialDataReadOptions
    {
        /// <summary>
        /// If true, zooms the map to the bounding box of the loaded data (if it exists).
        /// Default is 'true'.
        /// </summary>
        public bool ZoomToBBox { get; set; } = true;

        /// <summary>
        /// <see cref="BaseSpatialDataReadOptions"/>
        /// </summary>
        public BaseSpatialDataReadOptions? BaseSpatialDataReadOptions { get; set; }

        /// <summary>
        /// <see cref="BaseSpatialXmlReadOptions"/>
        /// </summary>
        public BaseSpatialXmlReadOptions? BaseSpatialXmlReadOptions { get; set; }

        /// <summary>
        /// <see cref="GmlReadOptions"/>
        /// </summary>
        public GmlReadOptions? GmlReadOptions { get; set; }

        /// <summary>
        /// <see cref="GpxReadOptions"/>
        /// </summary>
        public GpxReadOptions? GpxReadOptions { get; set; }

        /// <summary>
        /// <see cref="KmlReadOptions"/>
        /// </summary>
        public KmlReadOptions? KmlReadOptions { get; set; }

        /// <summary>
        /// <see cref="SpatialCsvReadOptions"/>
        /// </summary>
        public SpatialCsvReadOptions? SpatialCsvReadOptions { get; set; }
    }
}
