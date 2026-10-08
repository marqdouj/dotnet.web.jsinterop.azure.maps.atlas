namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Intl
{
    /// <summary>
    /// Represents the options for formatting a date/time value, analogous to JavaScript's Intl.DateTimeFormatOptions.
    /// </summary>
    public class DateTimeFormatOptions : ICloneable
    {
        /// <summary>
        /// "best fit" | "lookup" | undefined;
        /// </summary>
        public string? LocaleMatcher { get; set; }

        /// <summary>
        /// "long" | "short" | "narrow" | undefined;
        /// </summary>
        public string? Weekday { get; set; }

        /// <summary>
        /// "long" | "short" | "narrow" | undefined;
        /// </summary>
        public string? Era { get; set; }

        /// <summary>
        /// "numeric" | "2-digit" | undefined;
        /// </summary>
        public string? Year { get; set; }

        /// <summary>
        /// "numeric" | "2-digit" | "long" | "short" | "narrow" | undefined;
        /// </summary>
        public string? Month { get; set; }

        /// <summary>
        /// "numeric" | "2-digit" | undefined;
        /// </summary>
        public string? Day { get; set; }

        /// <summary>
        /// "numeric" | "2-digit" | undefined;
        /// </summary>
        public string? Hour { get; set; }

        /// <summary>
        /// "numeric" | "2-digit" | undefined;
        /// </summary>
        public string? Minute { get; set; }

        /// <summary>
        /// "numeric" | "2-digit" | undefined;
        /// </summary>
        public string? Second { get; set; }

        /// <summary>
        /// "short" | "long" | "shortOffset" | "longOffset" | "shortGeneric" | "longGeneric" | undefined;
        /// </summary>
        public string? TimeZoneName { get; set; }

        /// <summary>
        /// "best fit" | "basic" | undefined;
        /// </summary>
        public string? FormatMatcher { get; set; }

        /// <summary>
        /// 1 | 2 | 3 | undefined;
        /// </summary>
        public int? FractionalSecondDigits { get; set; }

        /// <summary>
        /// boolean | undefined;
        /// </summary>
        public bool? Hour12 { get; set; }

        /// <summary>
        /// string | undefined;
        /// </summary>
        public string? TimeZone { get; set; }

        /// <summary>
        /// string | undefined;
        /// </summary>
        public string? Calendar { get; set; }

        /// <summary>
        /// "narrow" | "short" | "long" | undefined;
        /// </summary>
        public string? DayPeriod { get; set; }

        /// <summary>
        /// string | undefined;
        /// </summary>
        public string? NumberingSystem { get; set; }

        /// <summary>
        /// "full" | "long" | "medium" | "short" | undefined;
        /// </summary>
        public string? DateStyle { get; set; }

        /// <summary>
        /// "full" | "long" | "medium" | "short" | undefined;
        /// </summary>
        public string? TimeStyle { get; set; }

        /// <summary>
        /// "h11" | "h12" | "h23" | "h24" | undefined;
        /// </summary>
        public string? HourCycle { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public object Clone()
        {
            var clone = (DateTimeFormatOptions)MemberwiseClone();
            return clone;
        }
    }
}