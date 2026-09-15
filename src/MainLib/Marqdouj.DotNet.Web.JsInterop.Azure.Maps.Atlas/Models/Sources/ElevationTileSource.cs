namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Sources
{
    /// <summary>
    /// <see cref="SourceType.ElevationTile"/>
    /// </summary>
    public class ElevationTileSource : MapSource
    {
        /// <summary>
        /// <inheritdoc cref="SourceType"/>
        /// </summary>
        public override SourceType? Type => SourceType.ElevationTile;

        /// <summary>
        /// <inheritdoc cref="ElevationTileSourceOptions"/>
        /// </summary>
        public ElevationTileSourceOptions? Options { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            var clone = (ElevationTileSource)MemberwiseClone();
            clone.Options = (ElevationTileSourceOptions?)Options?.Clone();
            return clone;
        }
    }
}
