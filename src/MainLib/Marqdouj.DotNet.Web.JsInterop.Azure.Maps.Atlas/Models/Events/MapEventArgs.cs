using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Types;
using Marqdouj.DotNet.Web.JsInterop.GeoJson;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events
{
    /// <summary>
    /// Event notification args.
    /// </summary>
    public class MapEventArgs
    {
        private protected static readonly JsonSerializerOptions serializerOptions = new(JsonSerializerDefaults.Web);

        /// <summary>
        /// The id of the html element where the map is displayed.
        /// </summary>
        [JsonInclude]
        public string MapId { get; internal set; } = default!;

        /// <summary>
        /// <see cref="MapEventTarget"/>
        /// </summary>
        [JsonInclude]
        public MapEventTarget Target { get; internal set; } = default!;

        /// <summary>
        /// The id of the target (if applicable).
        /// </summary>
        [JsonInclude]
        public string? TargetId { get; internal set; }

        /// <summary>
        /// They type of event.
        /// </summary>
        [JsonInclude]
        public string Type { get; internal set; } = default!;

        /// <summary>
        /// Tries to parse the <see cref="Type"/> into the specified Enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T? TypeToEnum<T>() where T: struct, Enum
        {
            if (Enum.TryParse<T>(Type, true, out var value))
                return value;

            return default!;
        }

        /// <summary>
        /// <see cref="GeoJsonProperties"/>
        /// </summary>
        [JsonInclude]
        public GeoJsonProperties? Payload { get; internal set; }

        /// <summary>
        /// Tries to get a value from the Payload.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>The item or null if not found.</returns>
        public T? GetPayloadItem<T>(string key) where T : class
        {
            var payloadItem = Payload?.FirstOrDefault(e => e.Key.Equals(key, StringComparison.OrdinalIgnoreCase)).Value;

            if (payloadItem != null)
            {
                if (payloadItem is JsonElement elem)
                {
                    var item = elem.Deserialize<T?>(serializerOptions);
                    return item;
                }
            }

            return null;
        }

        /// <summary>
        /// Indicates if the event is the <see cref="MapEventType.Error"/>.
        /// </summary>
        public bool IsErrorEvent => Target == MapEventTarget.Map && TypeToEnum<MapEventType>() == MapEventType.Error;

        /// <summary>
        /// Indicates if the event is the <see cref="MapEventType.Ready"/>.
        /// </summary>
        public bool IsReadyEvent => Target == MapEventTarget.Map && TypeToEnum<MapEventType>() == MapEventType.Ready;
    }
}
