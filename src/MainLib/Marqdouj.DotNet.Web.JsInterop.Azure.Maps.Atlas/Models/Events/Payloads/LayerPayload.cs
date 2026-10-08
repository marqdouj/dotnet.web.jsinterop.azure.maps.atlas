using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Events.Payloads
{
    /// <summary>
    /// Payload for a map/layer event.
    /// </summary>
    public class LayerPayload
    {
        /// <summary>
        /// The ID of the layer.
        /// </summary>
        [JsonInclude] public string? Id { get; internal set; }

        /// <summary>
        /// <inheritdoc cref="MousePayload"/>
        /// </summary>
        [JsonInclude] public MousePayload? Mouse { get; internal set; }

        /// <summary>
        /// <inheritdoc cref="TouchPayload"/>
        /// </summary>
        [JsonInclude] public TouchPayload? Touch { get; internal set; }

        /// <summary>
        /// <inheritdoc cref="WheelPayload"/>
        /// </summary>
        [JsonInclude] public WheelPayload? Wheel { get; internal set; }
    }
}
