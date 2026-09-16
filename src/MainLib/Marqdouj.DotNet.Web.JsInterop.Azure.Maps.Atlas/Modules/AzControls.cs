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
        /// <returns></returns>
        ValueTask Add(Map map, IEnumerable<MapControl> controls);

        /// <summary>
        /// Gets control references from the map based on <see cref="ControlType"/>.
        /// </summary>
        /// <param name="map"><see cref="Map"/></param>
        /// <param name="controls">List of control types to get. If null then all <see cref="ControlType"/> controls will be returned.</param>
        /// <returns></returns>
        ValueTask<List<IMapObjectReference>> GetControls(Map map, IEnumerable<ControlType>? controls = null);

        /// <summary>
        /// Remove controls from the map.
        /// </summary>
        /// <param name="map"><see cref="Map"/></param>
        /// <param name="controls">List of controls types to remove. <see cref="ControlType"/>.</param>
        /// <returns></returns>
        ValueTask Remove(Map map, IEnumerable<ControlType>? controls);

        /// <summary>
        /// Removes all controls based on the JsReference.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="controls"></param>
        /// <returns></returns>
        ValueTask RemoveControls(Map map, IEnumerable<IMapObjectReference> controls);
    }

    internal class AzControls(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasControls
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;

        public async ValueTask Add(Map map, IEnumerable<MapControl> controls)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync(GetJsInteropMethod(), map.MapReference, controls.Cast<object>().ToList());
        }

        public async ValueTask Remove(Map map, IEnumerable<ControlType>? controls)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync(GetJsInteropMethod(), map.MapReference, controls);
        }

        public async ValueTask RemoveControls(Map map, IEnumerable<IMapObjectReference> controls)
        {
            controls.ValidateReferenceType<ControlType>();

            var module = await moduleTask.Value;
            await module.InvokeVoidAsync(GetJsInteropMethod(), map.MapReference, controls.Select(c => c.JsReference));
        }

        public async ValueTask<List<IMapObjectReference>> GetControls(Map map, IEnumerable<ControlType>? controls = null)
        {
            var module = await moduleTask.Value;
            var items = await module.InvokeAsync<List<MapObjectReference>>(GetJsInteropMethod(), map.MapReference, controls);
            return [.. items.Cast<IMapObjectReference>()];
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.Controls.GetJsModuleMethod(name);
    }
}
