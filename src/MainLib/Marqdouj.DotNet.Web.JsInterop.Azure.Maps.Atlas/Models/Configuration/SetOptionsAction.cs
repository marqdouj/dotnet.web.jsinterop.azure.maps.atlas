using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Configuration
{
    /// <summary>
    /// The type of action to perform when updating map options.
    /// 'Replace' will replace all values defined in the class; null values are set to their defaults. 
    /// 'Update' will change existing values. Only those values that are not null will be updated.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<SetOptionsAction>))]
    public enum SetOptionsAction
    {
        /// <summary>
        /// Updates existing values. Only those values that are not null will be updated.
        /// </summary>
        Update,

        /// <summary>
        /// Replaces all values defined in the class; null values are set to their defaults.
        /// </summary>
        Replace,
    }
}
