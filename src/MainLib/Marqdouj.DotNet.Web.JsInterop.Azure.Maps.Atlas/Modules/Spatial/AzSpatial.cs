using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules.Spatial
{
    /// <summary>
    /// Interface for map spatial interactions.
    /// </summary>
    public interface IAtlasSpatial
    {
        /// <summary>
        /// <inheritdoc cref="IAtlasSpatialIO"/>
        /// </summary>
        IAtlasSpatialIO IO { get; }

        /// <summary>
        /// <inheritdoc cref="IAtlasSpatialLayers"/>
        /// </summary>
        IAtlasSpatialLayers Layers { get; }
    }

    internal class AzSpatial(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasSpatial
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;

        public IAtlasSpatialIO IO { get; } = new AzSpatialIO(moduleTask);
        public IAtlasSpatialLayers Layers { get; } = new AzSpatialLayers(moduleTask);

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.Spatial.GetJsModuleMethod(name);
    }
}
