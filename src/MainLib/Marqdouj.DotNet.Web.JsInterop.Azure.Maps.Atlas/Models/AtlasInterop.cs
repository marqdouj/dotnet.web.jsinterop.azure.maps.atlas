using Marqdouj.DotNet.JsInterop.AzureMaps.Models.Data;
using Marqdouj.DotNet.JsInterop.AzureMaps.Models.Math;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Marqdouj.DotNet.JsInterop.AzureMaps.Models
{
    internal class AtlasInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;
        private readonly DotNetObjectReference<ComponentBase> dotNetRef;

        public AtlasInterop(IJSRuntime jsRuntime, ComponentBase component)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./_content/Marqdouj.DotNet.JsInterop.AzureMaps/atlas.js").AsTask());
            dotNetRef = DotNetObjectReference.Create(component);

            Data = new AzData(moduleTask);
            Math = new AzMath(moduleTask);
        }

        public IAtlasData Data { get; }
        public IAtlasMath Math  { get; }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }

            ((IDisposable)dotNetRef)?.Dispose();
        }
    }
}
