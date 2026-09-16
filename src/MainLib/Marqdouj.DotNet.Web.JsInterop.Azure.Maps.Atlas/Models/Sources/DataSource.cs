namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Sources
{
    /// <summary>
    /// <see cref="SourceType.Data"/>
    /// </summary>
    public class DataSource : MapSource
    {
        /// <summary>
        /// <inheritdoc cref="SourceType"/>
        /// </summary>
        public override SourceType? Type => SourceType.Data;

        /// <summary>
        /// <inheritdoc cref="DataSourceOptions"/>
        /// </summary>
        public DataSourceOptions? Options { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            var clone = (DataSource)MemberwiseClone();
            clone.Options = (DataSourceOptions?)Options?.Clone();

            return clone;
        }
    }
}
