using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Controls;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules;
using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace Sandbox.CustomJs
{
    public sealed class CustomInterop(IJSRuntime jsRuntime) : IAsyncDisposable
    {
#pragma warning disable BL0016 // Unguarded JS interop call
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/Sandbox.CustomJs/customInterop.js").AsTask());
#pragma warning restore BL0016 // Unguarded JS interop call

        public async ValueTask AddControls(IJSObjectReference azmap, IEnumerable<MapControl> controls)
        {
            await moduleTask.InvokeVoidAsyncTC(GetCustomMapMethod(), azmap, controls.Cast<object>().ToList());
        }

        public async ValueTask RemoveControls(IJSObjectReference azmap, IEnumerable<MapControl> controls)
        {
            await moduleTask.InvokeVoidAsyncTC(GetCustomMapMethod(), azmap, controls.Cast<object>().ToList());
        }

        internal static string GetCustomMapMethod([CallerMemberName] string name = "")
        {
            return name.ToJsonName();
        }

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

    internal static class Extensions
    {
        /// <summary>
        /// first char must be lowercase
        /// </summary>
        public static string ToJsonName(this string name)
        {
            var firstChar = name[0].ToString().ToLower();
            var remainder = name[1..];
            return $"{firstChar}{remainder}";
        }
    }
}
