using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules
{
    internal class AzLayers(Lazy<Task<IJSObjectReference>> moduleTask)
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;


        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.Layers.GetJsModuleMethod(name);
    }
}
