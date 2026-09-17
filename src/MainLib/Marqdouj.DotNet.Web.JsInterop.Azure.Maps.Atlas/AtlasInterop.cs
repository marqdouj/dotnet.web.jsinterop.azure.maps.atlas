using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules.Data;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules.Events;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules.Sources;
using Microsoft.JSInterop;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas
{
    /// <summary>
    /// <inheritdoc cref="AtlasInterop"/>
    /// </summary>
    public interface IAtlasInterop : IAsyncDisposable
    {
        /// <summary>
        /// <inheritdoc cref="IAtlasControls"/>
        /// </summary>
        IAtlasControls Controls { get; }

        /// <summary>
        /// <inheritdoc cref="IAtlasData"/>
        /// </summary>
        IAtlasData Data { get; }

        /// <summary>
        /// <inheritdoc cref="IAtlasEvents"/>
        /// </summary>
        IAtlasEvents Events { get; }

        /// <summary>
        /// <inheritdoc cref="IAtlasFactory"/>
        /// </summary>
        IAtlasFactory Factory { get; }

        /// <summary>
        /// <inheritdoc cref="IAtlasFeatures"/>
        /// </summary>
        IAtlasFeatures Features { get; }

        /// <summary>
        /// <inheritdoc cref="IAtlasLayers"/>
        /// </summary>
        IAtlasLayers Layers { get; }

        /// <summary>
        /// <inheritdoc cref="IAtlasMap"/>
        /// </summary>
        IAtlasMap Map { get; }

        /// <summary>
        /// <inheritdoc cref="IAtlasMath"/>
        /// </summary>
        IAtlasMath Math { get; }

        /// <summary>
        /// <inheritdoc cref="IAtlasSources"/>
        /// </summary>
        IAtlasSources Sources { get; }
    }

    /// <summary>
    /// JSInterop interaction module for the Azure Maps SDK.
    /// </summary>
    public sealed class AtlasInterop : IAtlasInterop
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsRuntime"></param>
        public AtlasInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./_content/Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas/atlas.js").AsTask());

            Controls = new AzControls(moduleTask);
            Data = new AzData(moduleTask);
            Events = new AzEvents(moduleTask);
            Factory = new AzFactory(moduleTask);
            Features = new AzFeatures(moduleTask);
            Layers = new AzLayers(moduleTask);
            Map = new AzMap(moduleTask);
            Math = new AzMath(moduleTask);
            Sources = new AzSources(moduleTask);
        }

        /// <summary>
        /// <inheritdoc cref="IAtlasControls"/>
        /// </summary>
        public IAtlasControls Controls { get; }

        /// <summary>
        /// <inheritdoc cref="IAtlasData"/>
        /// </summary>
        public IAtlasData Data { get; }

        /// <summary>
        /// <inheritdoc cref="IAtlasEvents"/>
        /// </summary>
        public IAtlasEvents Events { get; }

        /// <summary>
        /// <inheritdoc cref="IAtlasFactory"/>
        /// </summary>
        public IAtlasFactory Factory { get; }

        /// <summary>
        /// <inheritdoc cref="IAtlasFeatures"/>
        /// </summary>
        public IAtlasFeatures Features { get; }

        /// <summary>
        /// <inheritdoc cref="IAtlasLayers"/>
        /// </summary>
        public IAtlasLayers Layers { get; }

        /// <summary>
        /// <inheritdoc cref="IAtlasMap"/>
        /// </summary>
        public IAtlasMap Map { get; }

        /// <summary>
        /// <inheritdoc cref="IAtlasMath"/>
        /// </summary>
        public IAtlasMath Math { get; }

        /// <summary>
        /// <inheritdoc cref="IAtlasSources"/>
        /// </summary>
        public IAtlasSources Sources { get; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
