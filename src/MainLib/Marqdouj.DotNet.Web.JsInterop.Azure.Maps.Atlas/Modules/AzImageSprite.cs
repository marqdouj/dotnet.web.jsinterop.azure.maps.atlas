using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models;
using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.ImageSprite;
using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules
{
    /// <summary>
    /// Interface for Azure Maps image sprites.
    /// </summary>
    public interface IAtlasImageSprite
    {
        /// <summary>
        /// Add an icon image to the map's image sprite for use with symbols and patterns.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="id"></param>
        /// <param name="icon">The image to add to the map's sprite. Can be a data URI, inline SVG, or image URL.</param>
        /// <param name="meta"><see cref="StyleImageMetadata"/></param>
        /// <param name="removeExisting">If true, if an image with the same id is found it will be replaced with the new image.</param>
        /// <returns></returns>
        ValueTask Add(Map map, string id, string icon, StyleImageMetadata? meta =  null, bool removeExisting = false);

        /// <summary>
        /// Clears all images from the map's sprite collection, effectively removing all custom images that have been added.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        ValueTask Clear(Map map);

        /// <summary>
        /// Creates an image in the map's sprite collection based on a predefined template. 
        /// The template defines the characteristics of the image, such as its shape, color, and other properties.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="templateDef"></param>
        /// <param name="removeExisting">If true, if an image with the same id is found it will be replaced with the new image.</param>
        /// <returns></returns>
        ValueTask<bool> CreateFromTemplate(Map map, ImageTemplate templateDef, bool removeExisting = false);

        /// <summary>
        /// Gets a list of all image IDs currently present in the map's sprite collection.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        ValueTask<List<string>> GetImageIds(Map map);

        /// <summary>
        /// Gets a value indicating whether an image with the specified ID exists in the map's sprite collection.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="templateDef"></param>
        /// <returns></returns>
        ValueTask<bool> HasImage(Map map, ImageTemplate templateDef);

        /// <summary>
        /// Gets a value indicating whether an image with the specified ID exists in the map's sprite collection.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        ValueTask<bool> HasImage(Map map, string id);

        /// <summary>
        /// Removes an image with the specified ID from the map's sprite collection, effectively deleting it from the map.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        ValueTask Remove(Map map, string id);
    }

    internal class AzImageSprite(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasImageSprite
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;

        public async ValueTask<bool> CreateFromTemplate(Map map, ImageTemplate templateDef, bool removeExisting = false)
        {
            return await moduleTask.InvokeAsyncTC<bool>(GetJsInteropMethod(), map.MapReference, templateDef, removeExisting);
        }

        public async ValueTask<bool> HasImage(Map map, ImageTemplate templateDef)
        {
            return await moduleTask.InvokeAsyncTC<bool>(GetJsInteropMethod(), map.MapReference, templateDef.Id);
        }

        public async ValueTask<bool> HasImage(Map map, string id)
        {
            return await moduleTask.InvokeAsyncTC<bool>(GetJsInteropMethod(), map.MapReference, id);
        }

        public async ValueTask Add(Map map, string id, string icon, StyleImageMetadata? meta = null, bool removeExisting = false)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference, id, icon, meta, removeExisting);
        }

        public async ValueTask Clear(Map map)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference);
        }

        public async ValueTask<List<string>> GetImageIds(Map map)
        {
            return await moduleTask.InvokeAsyncTC<List<string>>(GetJsInteropMethod(), map.MapReference);
        }

        public async ValueTask Remove(Map map, string id)
        {
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), map.MapReference, id);
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.ImageSprite.GetJsModuleMethod(name);
    }
}
