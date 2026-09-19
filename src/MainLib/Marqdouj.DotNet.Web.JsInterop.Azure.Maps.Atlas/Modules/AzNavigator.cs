using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules
{
    /// <summary>
    /// Interface for the browser navigator.
    /// </summary>
    public interface IAtlasNavigator
    {
        /// <summary>
        /// Copies the text to the navigator clipboard.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        ValueTask<CopyTextResult> CopyTextToClipboard(string text);
    }

    internal class AzNavigator(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasNavigator
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;

        public async ValueTask<CopyTextResult> CopyTextToClipboard(string text)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<CopyTextResult>(GetJsInteropMethod(), text);
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.Navigator.GetJsModuleMethod(name);
    }

    /// <summary>
    /// Result from <see cref="IAtlasNavigator.CopyTextToClipboard(string)"/>
    /// </summary>
    public class CopyTextResult
    {
        /// <summary>
        /// Indicates the copy operation was a success.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Message returned by the operation.
        /// </summary>
        public string? Message { get; set; }
    }
}