using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Common;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules.Spatial;
using Marqdouj.DotNet.Web.JsInterop.GeoJson;
using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Spatial
{
    /// <summary>
    /// Result returned by <see cref="IAtlasSpatialIO.LoadGPSTraceRoute{T}(DotNetObjectReference{T}, Map, GPSTraceRouteParameters)"/>.
    /// </summary>
    public sealed class GPSTraceRouteResult : IAsyncDisposable
    {
        /// <summary>
        /// <see cref="IJSObjectReference"/> to an animation.
        /// </summary>
        public IJSObjectReference? Animation { get; set; }

        /// <summary>
        /// The map camera center position after the data has been read and if the map camera was changed.
        /// <see cref="GPSTraceRouteParameters.ZoomToBBox"/>.
        /// </summary>
        public Position? Center { get; set; }

        /// <summary>
        /// <see cref="JsErrorInfo"/>
        /// </summary>
        [JsonInclude] public JsErrorInfo? Error { get; internal set; }

        /// <summary>
        /// The map camera zoom value after the data has been read and if the map camera was changed.
        /// <see cref="GPSTraceRouteParameters.ZoomToBBox"/>.
        /// </summary>
        public double? Zoom { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public async ValueTask DisposeAsync()
        {
            if (Animation != null)
                await Animation.DisposeAsync();
        }
    }
}
