using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Intl
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <summary>
    /// Represents the possible values for rounding priority in number formatting.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<NumberFormatDisplayOptionsTrailingZeroDisplay>))]
    public enum NumberFormatDisplayOptionsTrailingZeroDisplay
    {
        auto,
        stripIfInteger,
    }

    /// <summary>
    /// Represents the possible values for rounding priority in number formatting.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<NumberFormatDisplayOptionsRoundingMode>))]
    public enum NumberFormatDisplayOptionsRoundingMode
    {
        ceil, floor, expand, trunc, halfCeil, halfFloor, halfExpand, halfTrunc, halfEven,
    }

    /// <summary>
    /// Represents the possible values for rounding priority in number formatting.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<NumberFormatOptionsRoundingPriority>))]
    public enum NumberFormatOptionsRoundingPriority
    {
        auto,
        morePrecision,
        lessPrecision,
    }
    /// <summary>
    /// Represents the possible values for currency display in number formatting.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<NumberFormatOptionsCurrencyDisplay>))]
    public enum NumberFormatOptionsCurrencyDisplay
    {
        code,
        symbol,
        name,
    }

    /// <summary>
    /// Represents the possible values for sign display in number formatting.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<NumberFormatOptionsSignDisplay>))]
    public enum NumberFormatOptionsSignDisplay
    {
        auto,
        never,
        always,
        exceptZero,
    }

    /// <summary>
    /// Represents the possible values for style display in number formatting.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<NumberFormatOptionsStyle>))]
    public enum NumberFormatOptionsStyle
    {
        @decimal,
        percent,
        currency,
    }


    /// <summary>
    /// Represents the possible values for compact display.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<NumberFormatOptionsCompactDisplay>))]
    public enum NumberFormatOptionsCompactDisplay
    {
        @short,
        @long,
    }

    /// <summary>
    /// Represents the possible values for notation.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<NumberFormatOptionsNotation>))]
    public enum NumberFormatOptionsNotation
    {
        standard,
        scientific,
        engineering,
        compact,
    }

    /// <summary>
    /// Represents the possible values for unit display.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<NumberFormatOptionsUnitDisplay>))]
    public enum NumberFormatOptionsUnitDisplay
    {
        @short,
        @long,
        narrow,
    }

    /// <summary>
    /// Represents the possible values for currency sign.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<NumberFormatOptionsCurrencySign>))]
    public enum NumberFormatOptionsCurrencySign
    {
        standard,
        accounting,
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

    /// <summary>
    /// Represents the options for formatting a number, analogous to JavaScript's Intl.NumberFormatOptions.
    /// </summary>
    public class NumberFormatOptions : ICloneable
    {
        /// <summary>
        /// "lookup" | "best fit" | undefined;
        /// </summary>
        public string? LocaleMatcher { get; set; }

        /// <summary>
        /// <inheritdoc cref="NumberFormatOptionsStyle"/>
        /// </summary>
        public NumberFormatOptionsStyle? Style { get; set; }

        /// <summary>
        /// string | undefined;
        /// </summary>
        public string? Currency { get; set; }

        /// <summary>
        /// <inheritdoc cref="NumberFormatOptionsCurrencyDisplay"/>
        /// </summary>
        public NumberFormatOptionsCurrencyDisplay? CurrencyDisplay { get; set; }

        /// <summary>
        /// true | false | undefined.
        /// </summary>
        public bool? UseGrouping { get; set; }

        /// <summary>
        /// number | undefined;
        /// </summary>
        public int? MinimumIntegerDigits { get; set; }

        /// <summary>
        /// number | undefined;
        /// </summary>
        public int? MinimumFractionDigits { get; set; }

        /// <summary>
        /// number | undefined;
        /// </summary>
        public int? MaximumFractionDigits { get; set; }

        /// <summary>
        /// number | undefined;
        /// </summary>
        public int? MinimumSignificantDigits { get; set; }

        /// <summary>
        /// number | undefined;
        /// </summary>
        public int? MaximumSignificantDigits { get; set; }

        /// <summary>
        /// <inheritdoc cref="NumberFormatOptionsRoundingPriority"/>
        /// </summary>
        public NumberFormatOptionsRoundingPriority? RoundingPriority { get; set; }

        /// <summary>
        /// 1 | 2 | 5 | 10 | 20 | 25 | 50 | 100 | 200 | 250 | 500 | 1000 | 2000 | 2500 | 5000 | undefined;
        /// </summary>
        public int? RoundingIncrement { get; set; }

        /// <summary>
        /// <inheritdoc cref="NumberFormatDisplayOptionsRoundingMode"/>
        /// </summary>
        public NumberFormatDisplayOptionsRoundingMode? RoundingMode { get; set; }

        /// <summary>
        /// <inheritdoc cref="NumberFormatDisplayOptionsTrailingZeroDisplay"/>
        /// </summary>
        public NumberFormatDisplayOptionsTrailingZeroDisplay? TrailingZeroDisplay { get; set; }

        /// <summary>
        /// string | undefined;
        /// </summary>
        public string? NumberingSystem { get; set; }

        /// <summary>
        /// <inheritdoc cref="NumberFormatOptionsCompactDisplay"/>
        /// </summary>
        public NumberFormatOptionsCompactDisplay? CompactDisplay { get; set; }

        /// <summary>
        /// <inheritdoc cref="NumberFormatOptionsNotation"/>
        /// </summary>
        public NumberFormatOptionsNotation? Notation { get; set; }

        /// <summary>
        /// <inheritdoc cref="NumberFormatOptionsSignDisplay"/>
        /// </summary>
        public NumberFormatOptionsSignDisplay? SignDisplay { get; set; }

        /// <summary>
        /// string | undefined;
        /// </summary>
        public string? Unit { get; set; }

        /// <summary>
        /// <inheritdoc cref="NumberFormatOptionsUnitDisplay"/>
        /// </summary>
        public NumberFormatOptionsUnitDisplay? UnitDisplay { get; set; }

        /// <summary>
        /// <inheritdoc cref="NumberFormatOptionsCurrencySign"/>
        /// </summary>
        public NumberFormatOptionsCurrencySign? CurrencySign { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
