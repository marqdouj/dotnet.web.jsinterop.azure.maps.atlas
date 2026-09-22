using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Exceptions;
using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules
{
    internal enum JsModule
    {
        BoundingBox,
        Controls,
        DataSource,
        Factory,
        Features,
        LayerEvents,
        Layers,
        MarkerEvents,
        Markers,
        Math,
        Map,
        MapEvents,
        MercatorPoint,
        Navigator,
        Popups,
        Position,
        Sources,
        StyleControlEvents,
    }

    internal static class ModuleExtensions
    {
        internal static string GetJsModuleMethod(this JsModule module, [CallerMemberName] string name = "")
            => $"{module}.{name.ToJsonName()}";

        /// <summary>
        /// first char must be lowercase
        /// </summary>
        internal static string ToJsonName(this string name)
        {
            var firstChar = name[0].ToString().ToLower();
            var remainder = name[1..];
            return $"{firstChar}{remainder}";
        }

        internal static async ValueTask<T> InvokeAsyncInternal<T>(this Lazy<Task<IJSObjectReference>> moduleTask, string identifier, params object?[]? args)
        {
            try
            {
                var module = await moduleTask.Value;
                return await module.InvokeAsync<T>(identifier, args);
            }
            catch (JSException ex)
            {
                throw new JS2CSharpException(ex);
            }
        }

        internal static async ValueTask InvokeVoidAsyncInternal(this Lazy<Task<IJSObjectReference>> moduleTask, string identifier, params object?[]? args)
        {
            try
            {
                var module = await moduleTask.Value;
                await module.InvokeVoidAsync(identifier, args);
            }
            catch (JSException ex)
            {
                throw new JS2CSharpException(ex);
            }
        }

        private static string ParseMessage(this JSException ex)
        {
            var message = ex.Message;
            var index = message.IndexOf("Error:", StringComparison.OrdinalIgnoreCase);
            if (index > -1) 
            {
                message = message.Substring(0, index);
            }
            Console.WriteLine(ex.Message);
            return message;
        }
    }
}
