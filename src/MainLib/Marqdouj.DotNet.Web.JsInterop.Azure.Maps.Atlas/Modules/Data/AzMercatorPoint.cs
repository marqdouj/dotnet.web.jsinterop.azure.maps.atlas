using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Data;
using Marqdouj.DotNet.Web.JsInterop.GeoJson;
using Microsoft.JSInterop;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules.Data
{
    /// <summary>
    /// Interface for atlas.data.MercatorPoint interactions. <see href="https://learn.microsoft.com/en-us/javascript/api/azure-maps-control/atlas.data.mercatorpoint?view=azure-maps-typescript-latest"/>
    /// </summary>
    public interface IAtlasMercatorPoint
    {
        /// <summary>
        /// Converts a position into a mercator point.
        /// </summary>
        /// <param name="position">Position. May be an <see cref="IJSObjectReference"/>.</param>
        /// <returns></returns>
        [Display(Name = "From Position")]
        ValueTask<MercatorPoint> FromPosition(object position);

        /// <summary>
        /// Converts an array of positions into an array of mercator points.
        /// </summary>
        /// <param name="positions">IEnumerable{Position}. May be an <see cref="IJSObjectReference"/>.</param>
        /// <returns></returns>
        [Display(Name = "From Positions")]
        ValueTask<List<MercatorPoint>> FromPositions(object positions);

        /// <summary>
        /// Determine the Mercator scale factor for a given latitude.
        /// At the equator the scale factor will be 1, which increases at higher latitudes.
        /// <see href="https://en.wikipedia.org/wiki/Mercator_projection#Scale_factor"/>
        /// </summary>
        /// <param name="latitude"></param>
        /// <returns></returns>
        [Display(Name = "Mercator Scale")]
        ValueTask<double> MercatorScale(double latitude);

        /// <summary>
        /// Returns the distance of 1 meter in `MercatorPoint` units at this latitude.
        /// </summary>
        /// <remarks>For coordinates in real world units using meters, this naturally provides the scale to transform into `MercatorPoint`s.</remarks>
        /// <param name="latitude"></param>
        /// <returns>Distance of 1 meter in `MercatorPoint` units.</returns>
        [Display(Name = "Meter In Mercator Units")]
        ValueTask<double> MeterInMercatorUnits(double latitude);

        /// <summary>
        /// Converts an array of positions into a Float32Array of mercator xyz values.
        /// </summary>
        /// <param name="positions">IEnumerable{Position}. May be an <see cref="IJSObjectReference"/>.</param>
        /// <returns></returns>
        [Display(Name = "To Float32 Array")]
        ValueTask<Dictionary<string, float>> ToFloat32Array(object positions);

        /// <summary>
        /// Converts a mercator point into a map position.
        /// </summary>
        /// <param name="mercator">MercatorPoint. May be an <see cref="IJSObjectReference"/>.</param>
        /// <returns></returns>
        [Display(Name = "To Position")]
        ValueTask<Position> ToPosition(object mercator);

        /// <summary>
        /// Converts an array of mercator points into an array of map positions.
        /// </summary>
        /// <param name="mercators">IEnumerable{MercatorPoint}. May be an <see cref="IJSObjectReference"/>.</param>
        /// <returns></returns>
        [Display(Name = "To Positions")]
        ValueTask<List<Position>> ToPositions(object mercators);
    }

    internal class AzMercatorPoint(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasMercatorPoint
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;

        #region FromPosition

        public async ValueTask<MercatorPoint> FromPosition(object position)
        {
            return await moduleTask.InvokeAsyncInternal<MercatorPoint>(GetJsInteropMethod(), position);
        }

        public async ValueTask<List<MercatorPoint>> FromPositions(object positions)
        {
            return await moduleTask.InvokeAsyncInternal<List<MercatorPoint>>(GetJsInteropMethod(), positions);
        }

        #endregion

        #region ToPosition

        public async ValueTask<Position> ToPosition(object mercator)
        {
            return await moduleTask.InvokeAsyncInternal<Position>(GetJsInteropMethod(), mercator);
        }

        public async ValueTask<List<Position>> ToPositions(object mercators)
        {
            return await moduleTask.InvokeAsyncInternal<List<Position>>(GetJsInteropMethod(), mercators);
        }
        #endregion

        public async ValueTask<Dictionary<string, float>> ToFloat32Array(object positions)
        {
            return await moduleTask.InvokeAsyncInternal<Dictionary<string, float>>(GetJsInteropMethod(), positions);
        }

        public async ValueTask<double> MercatorScale(double latitude)
        {
            return await moduleTask.InvokeAsyncInternal<double>(GetJsInteropMethod(), latitude);
        }

        public async ValueTask<double> MeterInMercatorUnits(double latitude)
        {
            return await moduleTask.InvokeAsyncInternal<double>(GetJsInteropMethod(), latitude);
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => ModuleExtensions.GetJsModuleMethod(JsModule.MercatorPoint, name);
    }
}
