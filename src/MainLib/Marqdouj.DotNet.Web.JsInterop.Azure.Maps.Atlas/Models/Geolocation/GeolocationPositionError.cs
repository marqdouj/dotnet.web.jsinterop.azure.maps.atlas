using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Geolocation
{
    /// <summary>
    /// The reason for a Geolocation error, based on <see href="https://developer.mozilla.org/en-US/docs/Web/API/GeolocationPositionError"/>.
    /// </summary>
    public class GeolocationPositionError
    {
        /// <summary>
        /// Gets or sets the error code associated with the geolocation operation, if available.
        /// </summary>
        /// <remarks>A null value indicates that no error code is present. The meaning of the error code
        /// depends on the context in which it is used.</remarks>
        public int? Code { get; set; }

        /// <summary>
        /// Tries to get/set the <see cref="Code"/> base on <see cref="GeolocationPositionErrorCode"/>.
        /// </summary>
        [JsonIgnore] public GeolocationPositionErrorCode? CodeT
        {
            get => Enum.TryParse<GeolocationPositionErrorCode>(Code?.ToString(), true, out var result) ? result : null;
            set { Code = (int?)value; }
        }

        /// <summary>
        /// Gets or sets the error message associated with the geolocation operation, if available.
        /// </summary>
        /// <remarks>A null value indicates that no error message is present. The meaning of the error message
        /// depends on the context in which it is used.</remarks>
        public string? Message { get; set; }
    }
}
