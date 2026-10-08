using Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Animations;
using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Modules.Animations
{
    /// <summary>
    /// Interface for playable map animations.
    /// </summary>
    public interface IAtlasPlayableAnimation
    {
        /// <summary>
        /// Disposes the animation in JavaScript and the <see cref="IJSObjectReference"/> associated with it.
        /// Do not call this method if you want to reuse the animation, as it will destroy the animation and its reference.
        /// </summary>
        /// <param name="jsReference"></param>
        /// <returns></returns>
        ValueTask DisposeAnimation(IJSObjectReference jsReference);

        /// <summary>
        /// Gets the duration of the animation. Returns Infinity if the animations loops forever.
        /// </summary>
        /// <returns></returns>
        ValueTask<double> GetDuration(IJSObjectReference jsReference);

        /// <summary>
        /// Gets the options for the animation.
        /// </summary>
        /// <param name="jsReference"></param>
        /// <returns></returns>
        ValueTask<PlayableAnimationOptions> GetOptions(IJSObjectReference jsReference);

        /// <summary>
        /// Checks to see if the animation is playing.
        /// </summary>
        /// <returns></returns>
        ValueTask<bool> IsPlaying(IJSObjectReference jsReference);

        /// <summary>
        /// Pauses the animation.
        /// </summary>
        /// <returns></returns>
        ValueTask Pause(IJSObjectReference jsReference);

        /// <summary>
        /// Plays the animation.
        /// </summary>
        /// <returns></returns>
        ValueTask Play(IJSObjectReference jsReference);

        /// <summary>
        /// Resets the animation.
        /// </summary>
        /// <returns></returns>
        ValueTask Reset(IJSObjectReference jsReference);

        /// <summary>
        /// Sets the options for the animation.
        /// </summary>
        /// <param name="jsReference"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        ValueTask SetOptions(IJSObjectReference jsReference, PlayableAnimationOptions options);

        /// <summary>
        /// Stops the animation.
        /// </summary>
        /// <returns></returns>
        ValueTask Stop(IJSObjectReference jsReference);
    }

    internal class AzPlayableAnimation(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasPlayableAnimation
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;

        public ValueTask<bool> IsPlaying(IJSObjectReference jsReference)
        {
            ValidateJsReference(jsReference);
            return moduleTask.InvokeAsyncTC<bool>(GetJsInteropMethod(), jsReference);
        }

        public async ValueTask<double> GetDuration(IJSObjectReference jsReference)
        {
            ValidateJsReference(jsReference);
            return await moduleTask.InvokeAsyncTC<double>(GetJsInteropMethod(), jsReference);
        }

        public async ValueTask Pause(IJSObjectReference jsReference)
        {
            ValidateJsReference(jsReference);
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), jsReference);
        }

        public async ValueTask Play(IJSObjectReference jsReference)
        {
            ValidateJsReference(jsReference);
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), jsReference);
        }

        public async ValueTask Stop(IJSObjectReference jsReference)
        {
            ValidateJsReference(jsReference);
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), jsReference);
        }

        public async ValueTask Reset(IJSObjectReference jsReference)
        {
            ValidateJsReference(jsReference);
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), jsReference);
        }

        public async ValueTask<PlayableAnimationOptions> GetOptions(IJSObjectReference jsReference)
        {
            ValidateJsReference(jsReference);
            return await moduleTask.InvokeAsyncTC<PlayableAnimationOptions>(GetJsInteropMethod(), jsReference);
        }

        public async ValueTask SetOptions(IJSObjectReference jsReference, PlayableAnimationOptions options)
        {
            ValidateJsReference(jsReference);
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), jsReference, options);
        }

        public async ValueTask DisposeAnimation(IJSObjectReference jsReference)
        {
            ValidateJsReference(jsReference);
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), jsReference);
            await jsReference.DisposeAsync();
        }

        private static void ValidateJsReference(IJSObjectReference jsReference)
        {
            if (jsReference == null)
                throw new InvalidOperationException("The JS reference is not initialized.");
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.PlayableAnimation.GetJsModuleMethod(name);
    }
}
