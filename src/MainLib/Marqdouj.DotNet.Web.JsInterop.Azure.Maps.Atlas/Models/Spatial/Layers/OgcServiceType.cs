using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Spatial.Layers
{
    /// <summary>
    /// Ogc service types.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<OgcServiceType>))]
    public enum OgcServiceType
    {
        /// <summary>
        /// Indicates that the metadata object describes a map service accessed via OGC WMS (Web Map Service)
        /// </summary>
        WMS,

        /// <summary>
        /// Indicates that the metadata object describes a map service accessed via OHC WMTS (Web Map Tile Service).
        /// </summary>
        WMTS
    }
}
