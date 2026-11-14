using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Controls;
using Marqdouj.DotNet.Web.JsInterop.Extensions;
using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace Sandbox.CustomJs
{
    public sealed class CustomInterop(IJSRuntime jsRuntime) : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = jsRuntime.CreateModuleTask("./_content/Sandbox.CustomJs/customInterop.js");

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
