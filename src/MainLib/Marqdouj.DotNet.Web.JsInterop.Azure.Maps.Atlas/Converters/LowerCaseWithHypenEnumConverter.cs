using System.Text.Json;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Converters
{
    internal class LowerCaseWithHypenEnumConverter<T> : JsonConverter<T> where T : struct, Enum
    {
        private enum ConvertDirection { Read, Write }

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
                throw new JsonException($"Expected string for enum {typeof(T).Name}");

            var enumText = ToEnumString(reader.GetString(), ConvertDirection.Read);
            if (string.IsNullOrWhiteSpace(enumText))
                throw new JsonException("Enum string value cannot be null or empty");

            if (Enum.TryParse(enumText, ignoreCase: true, out T value))
                return value;

            throw new JsonException($"Unable to convert \"{enumText}\" to {typeof(T).Name}");
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            var enumName = ToEnumString(value.ToString(), ConvertDirection.Write);

            if (string.IsNullOrEmpty(enumName))
            {
                writer.WriteNullValue();
                return;
            }

            string lowerCaseName = enumName.ToLower();
            writer.WriteStringValue(lowerCaseName);
        }

        private static string? ToEnumString(string? enumString, ConvertDirection direction)
        {
            enumString = direction == ConvertDirection.Read ? enumString?.Replace("-", "_") : enumString?.Replace("_", "-").ToLower(); ;

            return enumString?.Trim();
        }
    }
}
