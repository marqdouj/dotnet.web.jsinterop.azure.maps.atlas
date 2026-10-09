using Marqdouj.DotNet.EnumConverters;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Intl
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    /// <summary>
    /// Represents the possible values for LocaleMatcher in datetime formatting.
    /// </summary>
    [JsonConverter(typeof(UnderscoreSpaceEnumConverter<DateTimeFormatOptionsLocaleMatcher>))]
    public enum DateTimeFormatOptionsLocaleMatcher
    {
        best_fit,
        lookup,
    }

    /// <summary>
    /// Represents the possible values for Weekday in datetime formatting.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<DateTimeFormatOptionsWeekday>))]
    public enum DateTimeFormatOptionsWeekday
    {
        @long,
        @short,
        narrow,
    }

    /// <summary>
    /// Represents the possible values for Era in datetime formatting.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<DateTimeFormatOptionsEra>))]
    public enum DateTimeFormatOptionsEra
    {
        @long,
        @short,
        narrow,
    }

    /// <summary>
    /// Represents the possible values for TimeZoneName in datetime formatting.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<DateTimeFormatOptionsTimeZoneName>))]
    public enum DateTimeFormatOptionsTimeZoneName
    {
        @short, 
        @long, 
        shortOffset, 
        longOffset, 
        shortGeneric, 
        longGeneric
    }

    /// <summary>
    /// Represents the possible values for FormatMatcher in datetime formatting.
    /// </summary>
    [JsonConverter(typeof(UnderscoreSpaceEnumConverter<DateTimeFormatOptionsFormatMatcher>))]
    public enum DateTimeFormatOptionsFormatMatcher
    {
        best_fit,
        basic,
    }

    /// <summary>
    /// Represents the possible values for DayPeriod in datetime formatting.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<DateTimeFormatOptionsDayPeriod>))]
    public enum DateTimeFormatOptionsDayPeriod
    {
        narrow,
        @short,
        @long,
    }

    /// <summary>
    /// Represents the possible values for DateStyle in datetime formatting.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<DateTimeFormatOptionsDateStyle>))]
    public enum DateTimeFormatOptionsDateStyle
    {
        full,
        @long,
        medium,
        @short,
    }

    /// <summary>
    /// Represents the possible values for TimeStyle in datetime formatting.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<DateTimeFormatOptionsTimeStyle>))]
    public enum DateTimeFormatOptionsTimeStyle
    {
        full,
        @long,
        medium,
        @short,
    }

    /// <summary>
    /// Represents the possible values for HourCycle in datetime formatting.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<DateTimeFormatOptionsHourCycle>))]
    public enum DateTimeFormatOptionsHourCycle
    {
        h11,
        h12,
        h23,
        h24,
    }

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

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
        /// Tries to get/set the <see cref="LocaleMatcher"/> using <see cref="DateTimeFormatOptionsLocaleMatcher"/>
        /// </summary>
        [JsonIgnore] 
        public DateTimeFormatOptionsLocaleMatcher? LocaleMatcherT 
        {
            get => Enum.TryParse<DateTimeFormatOptionsLocaleMatcher>(LocaleMatcher?.Replace(" ", "_"), true, out var result) ? result : null;
            set => LocaleMatcher = value?.ToString();
        }

        /// <summary>
        /// "long" | "short" | "narrow" | undefined;
        /// </summary>
        public string? Weekday { get; set; }

        /// <summary>
        /// Tries to get/set the <see cref="Weekday"/> using <see cref="DateTimeFormatOptionsWeekday"/>
        /// </summary>
        [JsonIgnore]
        public DateTimeFormatOptionsWeekday? WeekdayT
        {
            get => Enum.TryParse<DateTimeFormatOptionsWeekday>(Weekday, true, out var result) ? result : null;
            set => Weekday = value?.ToString();
        }

        /// <summary>
        /// "long" | "short" | "narrow" | undefined;
        /// </summary>
        public string? Era { get; set; }

        /// <summary>
        /// Tries to get/set the <see cref="Era"/> using <see cref="DateTimeFormatOptionsEra"/>
        /// </summary>
        [JsonIgnore]
        public DateTimeFormatOptionsEra? EraT
        {
            get => Enum.TryParse<DateTimeFormatOptionsEra>(Era, true, out var result) ? result : null;
            set => Era = value?.ToString();
        }

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
        /// Tries to get/set the <see cref="TimeZoneName"/> using <see cref="DateTimeFormatOptionsTimeZoneName"/>
        /// </summary>
        [JsonIgnore]
        public DateTimeFormatOptionsTimeZoneName? TimeZoneNameT
        {
            get => Enum.TryParse<DateTimeFormatOptionsTimeZoneName>(TimeZoneName, true, out var result) ? result : null;
            set => TimeZoneName = value?.ToString();
        }

        /// <summary>
        /// "best fit" | "basic" | undefined;
        /// </summary>
        public string? FormatMatcher { get; set; }

        /// <summary>
        /// Tries to get/set the <see cref="FormatMatcher"/> using <see cref="DateTimeFormatOptionsFormatMatcher"/>
        /// </summary>
        [JsonIgnore]
        public DateTimeFormatOptionsFormatMatcher? FormatMatcherT
        {
            get => Enum.TryParse<DateTimeFormatOptionsFormatMatcher>(FormatMatcher?.Replace(" ", "_"), true, out var result) ? result : null;
            set => FormatMatcher = value?.ToString();
        }

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
        /// Tries to get/set the <see cref="DayPeriod"/> using <see cref="DateTimeFormatOptionsDayPeriod"/>
        /// </summary>
        [JsonIgnore]
        public DateTimeFormatOptionsDayPeriod? DayPeriodT
        {
            get => Enum.TryParse<DateTimeFormatOptionsDayPeriod>(DayPeriod, true, out var result) ? result : null;
            set => DayPeriod = value?.ToString();
        }

        /// <summary>
        /// string | undefined;
        /// </summary>
        public string? NumberingSystem { get; set; }

        /// <summary>
        /// "full" | "long" | "medium" | "short" | undefined;
        /// </summary>
        public string? DateStyle { get; set; }

        /// <summary>
        /// Tries to get/set the <see cref="DateStyle"/> using <see cref="DateTimeFormatOptionsDateStyle"/>
        /// </summary>
        [JsonIgnore]
        public DateTimeFormatOptionsDateStyle? DateStyleT
        {
            get => Enum.TryParse<DateTimeFormatOptionsDateStyle>(DateStyle, true, out var result) ? result : null;
            set => DateStyle = value?.ToString();
        }

        /// <summary>
        /// "full" | "long" | "medium" | "short" | undefined;
        /// </summary>
        public string? TimeStyle { get; set; }

        /// <summary>
        /// Tries to get/set the <see cref="TimeStyle"/> using <see cref="DateTimeFormatOptionsTimeStyle"/>
        /// </summary>
        [JsonIgnore]
        public DateTimeFormatOptionsTimeStyle? TimeStyleT
        {
            get => Enum.TryParse<DateTimeFormatOptionsTimeStyle>(TimeStyle, true, out var result) ? result : null;
            set => TimeStyle = value?.ToString();
        }

        /// <summary>
        /// "h11" | "h12" | "h23" | "h24" | undefined;
        /// </summary>
        public string? HourCycle { get; set; }

        /// <summary>
        /// Tries to get/set the <see cref="HourCycle"/> using <see cref="DateTimeFormatOptionsHourCycle"/>.
        /// </summary>
        [JsonIgnore]
        public DateTimeFormatOptionsHourCycle? HourCycleT
        {
            get => Enum.TryParse<DateTimeFormatOptionsHourCycle>(HourCycle, true, out var result) ? result : null;
            set => HourCycle = value?.ToString();
        }

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