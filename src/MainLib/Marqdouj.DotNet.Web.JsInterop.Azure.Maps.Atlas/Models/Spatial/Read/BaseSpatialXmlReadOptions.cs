namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Spatial.Read
{
    /// <summary>
    /// Options that customize how XML files are read and parsed.
    /// </summary>
    public class BaseSpatialXmlReadOptions : BaseSpatialDataReadOptions
    {
        /// <summary>
        /// Specifies if style information should be parsed from the XML file and included as properties of the features.
        /// Default is 'true'.
        /// </summary>
        public bool? ParseStyles { get; set; }
    }
}
