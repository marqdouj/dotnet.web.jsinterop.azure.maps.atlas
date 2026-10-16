using Marqdouj.DotNet.Web.JsInterop.GeoJson;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Spatial.Read
{
    /// <summary>
    /// Options that customize how GML files are read and parsed.
    /// </summary>
    public class GmlReadOptions : BaseSpatialXmlReadOptions
    {
        /// <summary>
        /// If the reading a GML file, this specifies if the coordinate information is ordered 'longitude, latitude'.
        /// If set to false, coordinates will be parsed as "latitude, longitude".
        /// If unspecified, will try and determine based on hints within the GML,
        /// with a preference for 'latitude, longitude' ordering.
        /// </summary>
        public bool? IsAxisOrderLonLat { get; set; }

        /// <summary>
        /// If the reading a GML file, this contains a key-value pair list of property names to types
        /// which is used for deserializing custom properties.
        /// If a property name is not in the list, the property value will be parsed as a string.
        /// Typescript: Record{string, string | "string" | "number" | "boolean" | "date"}
        /// </summary>
        public GeoJsonProperties? PropertyTypes { get; set; }
    }
}
