namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Sources
{
    /// <summary>
    /// <see cref="SourceType.VectorTile"/>
    /// </summary>
    public class VectorTileSource : SourceBase
    {
        /// <summary>
        /// <inheritdoc cref="SourceType"/>
        /// </summary>
        public override SourceType? Type => SourceType.VectorTile;

        /// <summary>
        /// <inheritdoc cref="VectorTileSourceOptions"/>
        /// </summary>
        public VectorTileSourceOptions? Options { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            var clone = (VectorTileSource)MemberwiseClone();
            clone.Options = (VectorTileSourceOptions?)Options?.Clone();
            return clone;
        }
    }
}
