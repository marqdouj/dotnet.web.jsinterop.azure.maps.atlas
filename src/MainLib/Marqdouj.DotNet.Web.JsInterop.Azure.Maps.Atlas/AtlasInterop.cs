using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules.Data;
using Microsoft.JSInterop;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas
{
    /// <summary>
    /// <inheritdoc cref="AtlasInterop"/>
    /// </summary>
    public interface IAtlasInterop : IAsyncDisposable
    {
        /// <summary>
        /// <inheritdoc cref="IAtlasData"/>
        /// </summary>
        IAtlasData Data { get; }

        /// <summary>
        /// <inheritdoc cref="IAtlasMath"/>
        /// </summary>
        IAtlasMath Math { get; }
    }

    /// <summary>
    /// JSInterop interaction module for the Azure Maps SDK.
    /// </summary>
    public class AtlasInterop : IAtlasInterop
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

            Data = new AzData(moduleTask);
            Math = new AzMath(moduleTask);
        }

        /// <summary>
        /// <inheritdoc cref="IAtlasData"/>
        /// </summary>
        public IAtlasData Data { get; }

        /// <summary>
        /// <inheritdoc cref="IAtlasMath"/>
        /// </summary>
        public IAtlasMath Math { get; }

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
