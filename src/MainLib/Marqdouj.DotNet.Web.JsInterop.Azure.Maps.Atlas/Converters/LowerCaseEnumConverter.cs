using System.Text.Json;
using System.Text.Json.Serialization;

namespace Marqdouj.DotNet.Web.JsInterop.Azure.Maps.Atlas.Converters
{
    internal class LowerCaseEnumConverter<T> : JsonConverter<T> where T : struct, Enum
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
                throw new JsonException($"Expected string for enum {typeof(T).Name}");

            var enumText = reader.GetString();
            if (string.IsNullOrWhiteSpace(enumText))
                throw new JsonException("Enum string value cannot be null or empty");

            if (Enum.TryParse(enumText, ignoreCase: true, out T value))
                return value;

            throw new JsonException($"Unable to convert \"{enumText}\" to {typeof(T).Name}");
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            string enumName = value.ToString();
            if (string.IsNullOrEmpty(enumName))
            {
                writer.WriteNullValue();
                return;
            }

            var name = enumName.ToLower();
            writer.WriteStringValue(name);
        }
    }
}
