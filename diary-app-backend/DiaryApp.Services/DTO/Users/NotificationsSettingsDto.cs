using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiaryApp.Services.DTO.Users
{
    public class NotificationsSettingsDto : BaseDto
    {
        [Required]
        public int UserSettingsId { get; set; }
        public bool IsActivated { get; set; }
        public bool NotifyDayBefore { get; set; }
        [JsonConverter(typeof(JsonTimeSpanConverter))]
        public TimeSpan NotifyAt { get; set; }
    }

    public class JsonTimeSpanConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return TimeSpan.ParseExact(reader.GetString(), "c", CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("c", CultureInfo.InvariantCulture));
        }
    }
}
