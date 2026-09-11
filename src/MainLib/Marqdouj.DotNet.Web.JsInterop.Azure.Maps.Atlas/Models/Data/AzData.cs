using Microsoft.JSInterop;

namespace Marqdouj.DotNet.JsInterop.AzureMaps.Models.Data
{
    /// <summary>
    /// Interface for atlas.data interactions. <see href="https://learn.microsoft.com/en-us/javascript/api/azure-maps-control/atlas.data?view=azure-maps-typescript-latest"/>
    /// </summary>
    public interface IAtlasData
    {
        /// <summary>
        /// <inheritdoc cref="IAtlasBoundingBox"/>
        /// </summary>
        IAtlasBoundingBox BoundingBox { get; }
    }

    internal class AzData(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasData
    {
        public IAtlasBoundingBox BoundingBox { get; } = new AzBoundingBox(moduleTask);
    }
}
