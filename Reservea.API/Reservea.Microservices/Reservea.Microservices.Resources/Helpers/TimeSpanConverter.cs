using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Reservea.Microservices.Resources.Helpers
{
    public class TimeSpanConverter : JsonConverter<TimeSpan?>
    {
        public override TimeSpan? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var timeSpanString = reader.GetString();
            if (string.IsNullOrWhiteSpace(timeSpanString)) return null;

            return TimeSpan.Parse(timeSpanString, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                writer.WriteStringValue(value.Value.ToString(format: null, CultureInfo.InvariantCulture));
            }
            else
            {
                writer.WriteNullValue();
            }
        }
    }
}
