using Marqdouj.DotNet.Web.JsInterop.GeoJson;
using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules.Data
{
    /// <summary>
    /// Interface for atlas.data.Position interactions. <see href="https://learn.microsoft.com/en-us/javascript/api/azure-maps-control/atlas.data.position?view=azure-maps-typescript-latest"/>
    /// </summary>
    public interface IAtlasPosition
    {
        /// <summary>
        /// Compares the longitude and latitude values of two positions to see if they are equal, at a specified accuracy of decimal places.
        /// </summary>
        /// <param name="pos1">Position. My be an <see cref="IJSObjectReference"/>.</param>
        /// <param name="pos2">Position. My be an <see cref="IJSObjectReference"/>.</param>
        /// <param name="precision">The number of decimal places to compare to. Default: 6.</param>
        /// <returns>A boolean indicating if two positions to see if they are equal at the accuracy of the specified precision.</returns>
        ValueTask<bool> AreEqual(object pos1, object pos2, double? precision = null);

        /// <summary>
        /// Generates a Position object from latitude and longitude values.
        /// </summary>
        /// <param name="lat">The latitude value.</param>
        /// <param name="lng">The longitude value.</param>
        /// <param name="elv">The elevation value in meters.</param>
        /// <returns>A Position object that represents the provided LatLng information.</returns>
        ValueTask<Position> FromLatLng(double lat, double lng, double? elv = null);

        /// <summary>
        /// Generates a Position object from an object that contains coordinate information.
        /// The object is scanned for the following properties using a case insensitive test.
        /// Longitude: lng, longitude, lon, x
        /// Latitude: lat, latitude, y
        /// Elevation: elv, elevation, alt, altitude, z
        /// The object may also be an array of numbers that has the format; [lat, lng] or [lat, lng, elv]
        /// </summary>
        /// <param name="latLng">An object that contains coordinate information, 
        /// or an array that contains latitude/longitude information in the format; [lat, lng] or [lat, lng, elv].
        /// May be an <see cref="IJSObjectReference"/>.</param>
        /// <returns></returns>
        ValueTask<Position> FromLatLng(object latLng);

        /// <summary>
        /// Converts an array of objects that contain coordinate information into an array of Positions. Objects that can't be converted are discarded.
        /// Each object is either an array in the format; [lat, lng] or [lat, lng, elv], or an object with the any combination of the following properties:
        /// Longitude: lng, longitude, lon, x
        /// Latitude: lat, latitude, y
        /// Elevation: elv, elevation, alt, altitude, z 
        /// </summary>
        /// <param name="latLngs">The objects that contain coordinate information. May be an <see cref="IJSObjectReference"/>.</param>
        /// <returns>An array of Position objects that contain all the provided coordinate information.</returns>
        ValueTask<List<Position>> FromLatLngs(object latLngs);

        /// <summary>
        /// Clones a position.
        /// </summary>
        /// <param name="position">Position. My be an <see cref="IJSObjectReference"/>.</param>
        /// <returns></returns>
        ValueTask<Position> FromPosition(object position);
    }

    internal class AzPosition(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasPosition
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;

        public async ValueTask<bool> AreEqual(object pos1, object pos2, double? precision = null)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<bool>(GetJsInteropMethod(), pos1, pos2, precision);
        }

        public async ValueTask<Position> FromLatLng(object latLng)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<Position>(GetJsInteropMethod(), latLng);
        }

        public async ValueTask<Position> FromLatLng(double lat, double lng, double? elv = null)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<Position>(GetJsInteropMethod($"{nameof(FromLatLng)}Values"), lat, lng, elv);
        }

        public async ValueTask<List<Position>> FromLatLngs(object latLng)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<List<Position>>(GetJsInteropMethod(), latLng);
        }

        public async ValueTask<Position> FromPosition(object position)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<Position>(GetJsInteropMethod(), position);
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.Position.GetJsModuleMethod(name);
    }
}
