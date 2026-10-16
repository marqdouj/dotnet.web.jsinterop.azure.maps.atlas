namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Spatial.Read
{
    /// <summary>
    /// Column header definition for a delimited file.
    /// </summary>
    public class CsvHeader
    {
        /// <summary>
        /// The name of each column.
        /// </summary>
        public List<string>? Names { get; set; }

        /// <summary>
        /// The type of each column; string, number, boolean, date, geography (Well Known Text string).
        /// If unspecified or null, will default to string.
        /// </summary>
        public List<string>? Types { get; set; }
    }
}
