using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Common;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Controls;
using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules
{
    /// <summary>
    /// Interface for map control interactions.
    /// </summary>
    public interface IAtlasControls
    {
        /// <summary>
        /// Adds controls to the map.
        /// </summary>
        /// <param name="map"><see cref="Map"/></param>
        /// <param name="controls">List of controls to add.</param>
        /// <param name="getReferences">
        /// If true, then return the <see cref="IMapObjectReference"/> for the controls. Default is false.
        /// IMPORTANT! Ensure the <see cref="IMapObjectReference"/> items are disposed when you are done with them.
        /// </param>
        /// <returns></returns>
        ValueTask<List<IMapObjectReference>> Add(Map map, IEnumerable<MapControl> controls, bool getReferences = false);

        /// <summary>
        /// Gets control references.
        /// IMPORTANT! Ensure the <see cref="IMapObjectReference"/> items are disposed when you are done with them.
        /// </summary>
        /// <param name="map"><see cref="Map"/></param>
        /// <param name="controls"></param>
        /// <returns></returns>
        ValueTask<List<IMapObjectReference>> GetReferences(Map map, IEnumerable<MapControl> controls);

        /// <summary>
        /// Remove controls from the map.
        /// </summary>
        /// <param name="map"><see cref="Map"/></param>
        /// <param name="controls">List of controls types to remove. <see cref="ControlType"/>.</param>
        /// <returns></returns>
        ValueTask Remove(Map map, IEnumerable<MapControl> controls);
    }

    internal class AzControls(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasControls
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;

        public async ValueTask<List<IMapObjectReference>> Add(Map map, IEnumerable<MapControl> controls, bool getReferences = false)
        {
            var results = await moduleTask.InvokeAsyncTC<List<MapObjectReference>>(GetJsInteropMethod(), map.MapReference, controls.Cast<object>().ToList(), getReferences);
            return [.. results.Cast<IMapObjectReference>()];
        }

        public async ValueTask Remove(Map map, IEnumerable<MapControl> controls)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference, controls);
        }

        public async ValueTask<List<IMapObjectReference>> GetReferences(Map map, IEnumerable<MapControl> controls)
        {
            var items = await moduleTask.InvokeAsyncTC<List<MapObjectReference>>(GetJsInteropMethod(), map.MapReference, controls);
            return [.. items.Cast<IMapObjectReference>()];
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.Controls.GetJsModuleMethod(name);
    }
}
