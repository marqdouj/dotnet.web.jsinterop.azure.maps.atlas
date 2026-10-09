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
        lookup,
        best_fit,
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

    /// <summary>
    /// Some of the <see cref="NumberFormatOptions"/> string properties have an associated Enum.
    /// This interface allows you to get/set the string values using the associated Enum.
    /// If the Enum is missing a newer value that is required (the Enum has not yet been updated) 
    /// then you can set the <see cref="NumberFormatOptions"/> string property directly without using this interface.
    /// </summary>
    public interface INumberFormatOptionsT
    {
        NumberFormatOptionsCompactDisplay? CompactDisplay { get; set; }
        string? Currency { get; set; }
        NumberFormatOptionsCurrencyDisplay? CurrencyDisplay { get; set; }
        NumberFormatOptionsCurrencySign? CurrencySign { get; set; }
        NumberFormatOptionsLocaleMatcher? LocaleMatcher { get; set; }
        int? MaximumFractionDigits { get; set; }
        int? MaximumSignificantDigits { get; set; }
        int? MinimumFractionDigits { get; set; }
        int? MinimumIntegerDigits { get; set; }
        int? MinimumSignificantDigits { get; set; }
        NumberFormatOptionsNotation? Notation { get; set; }
        string? NumberingSystem { get; set; }
        int? RoundingIncrement { get; set; }
        NumberFormatOptionsRoundingMode? RoundingMode { get; set; }
        NumberFormatOptionsRoundingPriority? RoundingPriority { get; set; }
        NumberFormatOptionsSignDisplay? SignDisplay { get; set; }
        NumberFormatOptionsStyle? Style { get; set; }
        NumberFormatOptionsTrailingZeroDisplay? TrailingZeroDisplay { get; set; }
        string? Unit { get; set; }
        NumberFormatOptionsUnitDisplay? UnitDisplay { get; set; }
        bool? UseGrouping { get; set; }

        object Clone();
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

    /// <summary>
    /// Represents the options for formatting a number, analogous to JavaScript's Intl.NumberFormatOptions.
    /// </summary>
    public class NumberFormatOptions : ICloneable, INumberFormatOptionsT
    {
        /// <summary>
        /// "lookup" | "best fit" | undefined;
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

        NumberFormatOptionsLocaleMatcher? INumberFormatOptionsT.LocaleMatcher
        {
            get => Enum.TryParse<NumberFormatOptionsLocaleMatcher>(LocaleMatcher, true, out var result) ? result : null;
            set => LocaleMatcher = value?.ToString();
        }

        NumberFormatOptionsCompactDisplay? INumberFormatOptionsT.CompactDisplay 
        { 
            get => Enum.TryParse<NumberFormatOptionsCompactDisplay>(CompactDisplay, true, out var result) ? result : null; 
            set => CompactDisplay = value?.ToString();
        }

        NumberFormatOptionsCurrencyDisplay? INumberFormatOptionsT.CurrencyDisplay 
        { 
            get => Enum.TryParse<NumberFormatOptionsCurrencyDisplay>(CurrencyDisplay, true, out var result) ? result : null; 
            set => CurrencyDisplay = value?.ToString();
        }

        NumberFormatOptionsCurrencySign? INumberFormatOptionsT.CurrencySign 
        { 
            get => Enum.TryParse<NumberFormatOptionsCurrencySign>(CurrencySign, true, out var result) ? result : null;
            set => CurrencySign = value?.ToString(); 
        }

        NumberFormatOptionsNotation? INumberFormatOptionsT.Notation 
        { 
            get => Enum.TryParse<NumberFormatOptionsNotation>(Notation, true, out var result) ? result : null;
            set => Notation = value?.ToString(); 
        }

        NumberFormatOptionsRoundingMode? INumberFormatOptionsT.RoundingMode 
        { 
            get => Enum.TryParse<NumberFormatOptionsRoundingMode>(RoundingMode, true, out var result) ? result : null;
            set => RoundingMode = value?.ToString(); 
        }

        NumberFormatOptionsRoundingPriority? INumberFormatOptionsT.RoundingPriority 
        { 
            get => Enum.TryParse<NumberFormatOptionsRoundingPriority>(RoundingPriority, true, out var result) ? result : null;
            set => RoundingPriority = value?.ToString(); 
        }

        NumberFormatOptionsSignDisplay? INumberFormatOptionsT.SignDisplay 
        { 
            get => Enum.TryParse<NumberFormatOptionsSignDisplay>(SignDisplay, true, out var result) ? result : null;
            set => SignDisplay = value?.ToString(); 
        }

        NumberFormatOptionsStyle? INumberFormatOptionsT.Style 
        { 
            get => Enum.TryParse<NumberFormatOptionsStyle>(Style, true, out var result) ? result : null;
            set => Style = value?.ToString(); 
        }

        NumberFormatOptionsTrailingZeroDisplay? INumberFormatOptionsT.TrailingZeroDisplay 
        { 
            get => Enum.TryParse<NumberFormatOptionsTrailingZeroDisplay>(TrailingZeroDisplay, true, out var result) ? result : null;
            set => TrailingZeroDisplay = value?.ToString(); 
        }

        NumberFormatOptionsUnitDisplay? INumberFormatOptionsT.UnitDisplay 
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
