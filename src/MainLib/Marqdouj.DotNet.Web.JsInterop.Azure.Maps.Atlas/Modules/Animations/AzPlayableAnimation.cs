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
        /// <param name="animation"></param>
        /// <returns></returns>
        ValueTask DisposeAnimation(IJSObjectReference animation);

        /// <summary>
        /// Gets the duration of the animation. Returns Infinity if the animations loops forever.
        /// </summary>
        /// <returns></returns>
        ValueTask<double> GetDuration(IJSObjectReference animation);

        /// <summary>
        /// Gets the options for the animation.
        /// </summary>
        /// <param name="animation"></param>
        /// <returns></returns>
        ValueTask<PlayableAnimationOptions> GetOptions(IJSObjectReference animation);

        /// <summary>
        /// Checks to see if the animation is playing.
        /// </summary>
        /// <returns></returns>
        ValueTask<bool> IsPlaying(IJSObjectReference animation);

        /// <summary>
        /// Pauses the animation.
        /// </summary>
        /// <returns></returns>
        ValueTask Pause(IJSObjectReference animation);

        /// <summary>
        /// Plays the animation.
        /// </summary>
        /// <returns></returns>
        ValueTask Play(IJSObjectReference animation);

        /// <summary>
        /// Resets the animation.
        /// </summary>
        /// <returns></returns>
        ValueTask Reset(IJSObjectReference animation);

        /// <summary>
        /// Sets the options for the animation.
        /// </summary>
        /// <param name="animation"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        ValueTask SetOptions(IJSObjectReference animation, PlayableAnimationOptions options);

        /// <summary>
        /// Stops the animation.
        /// </summary>
        /// <returns></returns>
        ValueTask Stop(IJSObjectReference animation);
    }

    internal class AzPlayableAnimation(Lazy<Task<IJSObjectReference>> moduleTask) : IAtlasPlayableAnimation
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = moduleTask;

        public async ValueTask<bool> IsPlaying(IJSObjectReference animation)
        {
            ValidateJsReference(animation);
            return await moduleTask.InvokeAsyncTC<bool>(GetJsInteropMethod(), animation);
        }

        public async ValueTask<double> GetDuration(IJSObjectReference animation)
        {
            ValidateJsReference(animation);
            return await moduleTask.InvokeAsyncTC<double>(GetJsInteropMethod(), animation);
        }

        public async ValueTask Pause(IJSObjectReference animation)
        {
            ValidateJsReference(animation);
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), animation);
        }

        public async ValueTask Play(IJSObjectReference animation)
        {
            ValidateJsReference(animation);
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), animation);
        }

        public async ValueTask Stop(IJSObjectReference animation)
        {
            ValidateJsReference(animation);
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), animation);
        }

        public async ValueTask Reset(IJSObjectReference animation)
        {
            ValidateJsReference(animation);
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), animation);
        }

        public async ValueTask<PlayableAnimationOptions> GetOptions(IJSObjectReference animation)
        {
            ValidateJsReference(animation);
            return await moduleTask.InvokeAsyncTC<PlayableAnimationOptions>(GetJsInteropMethod(), animation);
        }

        public async ValueTask SetOptions(IJSObjectReference animation, PlayableAnimationOptions options)
        {
            ValidateJsReference(animation);
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), animation, options);
        }

        public async ValueTask DisposeAnimation(IJSObjectReference animation)
        {
            ValidateJsReference(animation);
            await moduleTask.InvokeVoidAsyncTC(GetJsInteropMethod(), animation);
            await animation.DisposeAsync();
        }

        private static void ValidateJsReference(IJSObjectReference animation)
        {
            if (animation == null)
                throw new InvalidOperationException("The animation is not initialized.");
        }

        private static string GetJsInteropMethod([CallerMemberName] string name = "")
            => JsModule.PlayableAnimation.GetJsModuleMethod(name);
    }
}
