using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Spatial.Read
{
    /// <summary>
    /// The action to perform with the read data on the datasource.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<DataSourceReadAction>))]
    public enum DataSourceReadAction
    {
        /// <summary>
        /// Add the data to the datasource with 'add' method.
        /// </summary>
        add,

        /// <summary>
        /// Add the data to the datasource with 'Add' setShapes.
        /// </summary>
        setShapes,
    }
}
