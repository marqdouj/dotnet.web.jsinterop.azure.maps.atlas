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
        public string? Id { get; set; }

        /// <summary>
        /// <inheritdoc cref="MousePayload"/>
        /// </summary>
        public MousePayload? Mouse { get; set; }

        /// <summary>
        /// <inheritdoc cref="TouchPayload"/>
        /// </summary>
        public TouchPayload? Touch { get; set; }

        /// <summary>
        /// <inheritdoc cref="WheelPayload"/>
        /// </summary>
        public WheelPayload? Wheel { get; set; }
    }
}
