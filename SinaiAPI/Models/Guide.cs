using System.Text.Json.Serialization;

namespace SinaiAPI.Models
{
    public class Guide
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LanguageEnum? Language { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum LanguageEnum
        {
            English,
            German,
        }

    }
}
