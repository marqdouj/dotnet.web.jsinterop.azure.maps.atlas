using Microsoft.JSInterop;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules.Events
{
    /// <summary>
    /// Interface for events.
    /// </summary>
    public interface IAtlasEvents
    {
        /// <summary>
        /// <inheritdoc cref="IAtlasLayerEvents"/>
        /// </summary>
        IAtlasLayerEvents Layers { get; }

        /// <summary>
        /// <inheritdoc cref="IAtlasMapEvents"/>
        /// </summary>
        IAtlasMapEvents Map { get; }
    }

    internal class AzEvents(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasEvents
    {
        public IAtlasLayerEvents Layers { get; } = new AzLayerEvents(moduleTask);
        public IAtlasMapEvents Map { get; } = new AzMapEvents(moduleTask);
    }
}
