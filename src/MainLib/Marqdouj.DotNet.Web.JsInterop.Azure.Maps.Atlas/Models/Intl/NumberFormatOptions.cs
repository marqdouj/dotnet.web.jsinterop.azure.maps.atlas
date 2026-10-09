using Marqdouj.DotNet.EnumConverters;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Models.Intl
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    /// <summary>
    /// Represents the possible values for locale matcher in number formatting.
    /// </summary>
    [JsonConverter(typeof(UnderscoreSpaceEnumConverter<NumberFormatOptionsLocaleMatcher>))]
    public enum NumberFormatOptionsLocaleMatcher
    {
        best_fit,
        lookup,
    }

    /// <summary>
    /// Represents the possible values for rounding priority in number formatting.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<NumberFormatOptionsTrailingZeroDisplay>))]
    public enum NumberFormatOptionsTrailingZeroDisplay
    {
        auto,
        stripIfInteger,
    }

    /// <summary>
    /// Represents the possible values for rounding priority in number formatting.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<NumberFormatOptionsRoundingMode>))]
    public enum NumberFormatOptionsRoundingMode
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
        /// <inheritdoc cref="NumberFormatOptionsLocaleMatcher"/>
        /// </summary>
        public string? LocaleMatcher { get; set; }

        /// <summary>
        /// <inheritdoc cref="NumberFormatOptionsStyle"/>
        /// </summary>
        public string? Style { get; set; }

        /// <summary>
        /// string | undefined;
        /// </summary>
        public string? Currency { get; set; }

        /// <summary>
        /// <inheritdoc cref="NumberFormatOptionsCurrencyDisplay"/>
        /// </summary>
        public string? CurrencyDisplay { get; set; }

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
        public string? RoundingPriority { get; set; }

        /// <summary>
        /// 1 | 2 | 5 | 10 | 20 | 25 | 50 | 100 | 200 | 250 | 500 | 1000 | 2000 | 2500 | 5000 | undefined;
        /// </summary>
        public int? RoundingIncrement { get; set; }

        /// <summary>
        /// <inheritdoc cref="NumberFormatOptionsRoundingMode"/>
        /// </summary>
        public string? RoundingMode { get; set; }

        /// <summary>
        /// <inheritdoc cref="NumberFormatOptionsTrailingZeroDisplay"/>
        /// </summary>
        public string? TrailingZeroDisplay { get; set; }

        /// <summary>
        /// string | undefined;
        /// </summary>
        public string? NumberingSystem { get; set; }

        /// <summary>
        /// <inheritdoc cref="NumberFormatOptionsCompactDisplay"/>
        /// </summary>
        public string? CompactDisplay { get; set; }

        /// <summary>
        /// <inheritdoc cref="NumberFormatOptionsNotation"/>
        /// </summary>
        public string? Notation { get; set; }

        /// <summary>
        /// <inheritdoc cref="NumberFormatOptionsSignDisplay"/>
        /// </summary>
        public string? SignDisplay { get; set; }

        /// <summary>
        /// string | undefined;
        /// </summary>
        public string? Unit { get; set; }

        /// <summary>
        /// <inheritdoc cref="NumberFormatOptionsUnitDisplay"/>
        /// </summary>
        public string? UnitDisplay { get; set; }

        /// <summary>
        /// <inheritdoc cref="NumberFormatOptionsCurrencySign"/>
        /// </summary>
        public string? CurrencySign { get; set; }

        /// <summary>
        /// Tries to get/set the <see cref="LocaleMatcher"/> using <see cref="NumberFormatOptionsLocaleMatcher"/>
        /// </summary>
        [JsonIgnore]
        public NumberFormatOptionsLocaleMatcher? LocaleMatcherT
        {
            get => Enum.TryParse<NumberFormatOptionsLocaleMatcher>(LocaleMatcher?.Replace(" ", "_"), true, out var result) ? result : null;
            set => LocaleMatcher = value?.ToString().Replace("_", " ");
        }

        /// <summary>
        /// Tries to get/set the <see cref="CompactDisplay"/> using <see cref="NumberFormatOptionsCompactDisplay"/>.
        /// </summary>
        [JsonIgnore]
        public NumberFormatOptionsCompactDisplay? CompactDisplayT
        {
            get => Enum.TryParse<NumberFormatOptionsCompactDisplay>(CompactDisplay, true, out var result) ? result : null;
            set => CompactDisplay = value?.ToString();
        }

        /// <summary>
        /// Tries to get/set the <see cref="CurrencyDisplay"/> using <see cref="NumberFormatOptionsCurrencyDisplay"/>.
        /// </summary>
        [JsonIgnore]
        public NumberFormatOptionsCurrencyDisplay? CurrencyDisplayT
        {
            get => Enum.TryParse<NumberFormatOptionsCurrencyDisplay>(CurrencyDisplay, true, out var result) ? result : null;
            set => CurrencyDisplay = value?.ToString();
        }

        /// <summary>
        /// Tries to get/set the <see cref="CurrencySign"/> using <see cref="NumberFormatOptionsCurrencySign"/>.
        /// </summary>
        [JsonIgnore]
        public NumberFormatOptionsCurrencySign? CurrencySignT
        {
            get => Enum.TryParse<NumberFormatOptionsCurrencySign>(CurrencySign, true, out var result) ? result : null;
            set => CurrencySign = value?.ToString();
        }

        /// <summary>
        /// Tries to get/set the <see cref="Notation"/> using <see cref="NumberFormatOptionsNotation"/>.
        /// </summary>
        [JsonIgnore]
        public NumberFormatOptionsNotation? NotationT
        {
            get => Enum.TryParse<NumberFormatOptionsNotation>(Notation, true, out var result) ? result : null;
            set => Notation = value?.ToString();
        }

        /// <summary>
        /// Tries to get/set the <see cref="RoundingMode"/> using <see cref="NumberFormatOptionsRoundingMode"/>.
        /// </summary>
        [JsonIgnore]
        public NumberFormatOptionsRoundingMode? RoundingModeT
        {
            get => Enum.TryParse<NumberFormatOptionsRoundingMode>(RoundingMode, true, out var result) ? result : null;
            set => RoundingMode = value?.ToString();
        }

        /// <summary>
        /// Tries to get/set the <see cref="RoundingPriority"/> using <see cref="NumberFormatOptionsRoundingPriority"/>.
        /// </summary>
        [JsonIgnore]
        public NumberFormatOptionsRoundingPriority? RoundingPriorityT
        {
            get => Enum.TryParse<NumberFormatOptionsRoundingPriority>(RoundingPriority, true, out var result) ? result : null;
            set => RoundingPriority = value?.ToString();
        }

        /// <summary>
        /// Tries to get/set the <see cref="SignDisplay"/> using <see cref="NumberFormatOptionsSignDisplay"/>.
        /// </summary>
        [JsonIgnore]
        public NumberFormatOptionsSignDisplay? SignDisplayT
        {
            get => Enum.TryParse<NumberFormatOptionsSignDisplay>(SignDisplay, true, out var result) ? result : null;
            set => SignDisplay = value?.ToString();
        }

        /// <summary>
        /// Tries to get/set the <see cref="Style"/> using <see cref="NumberFormatOptionsStyle"/>.
        /// </summary>
        [JsonIgnore]
        public NumberFormatOptionsStyle? StyleT
        {
            get => Enum.TryParse<NumberFormatOptionsStyle>(Style, true, out var result) ? result : null;
            set => Style = value?.ToString();
        }

        /// <summary>
        /// Tries to get/set the <see cref="TrailingZeroDisplay"/> using <see cref="NumberFormatOptionsTrailingZeroDisplay"/>.
        /// </summary>
        [JsonIgnore]
        public NumberFormatOptionsTrailingZeroDisplay? TrailingZeroDisplayT
        {
            get => Enum.TryParse<NumberFormatOptionsTrailingZeroDisplay>(TrailingZeroDisplay, true, out var result) ? result : null;
            set => TrailingZeroDisplay = value?.ToString();
        }

        /// <summary>
        /// Tries to get/set the <see cref="UnitDisplay"/> using <see cref="NumberFormatOptionsUnitDisplay"/>.
        /// </summary>
        [JsonIgnore]
        public NumberFormatOptionsUnitDisplay? UnitDisplayT
        {
            get => Enum.TryParse<NumberFormatOptionsUnitDisplay>(UnitDisplay, true, out var result) ? result : null;
            set => UnitDisplay = value?.ToString();
        }

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
