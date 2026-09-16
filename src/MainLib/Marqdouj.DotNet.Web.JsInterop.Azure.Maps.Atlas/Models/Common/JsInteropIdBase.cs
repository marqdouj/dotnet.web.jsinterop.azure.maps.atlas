namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Common
{
    /// <summary>
    /// Base class from JSInterop map objects that require an Id.
    /// </summary>
    public abstract class JsInteropIdBase : ICloneable
    {
        /// <summary>
        /// Indentifier for map object.
        /// </summary>
        public string Id { get; set { if (string.IsNullOrWhiteSpace(value)) throw new Exception($"{nameof(Id)} must have a value."); field = value.Trim(); } } = $"g_{Guid.CreateVersion7()}";

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public abstract object Clone();
    }
}
