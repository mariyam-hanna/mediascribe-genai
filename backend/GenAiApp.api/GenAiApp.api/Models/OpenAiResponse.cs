using System.Text.Json.Serialization;

namespace GenAiApp.api.Models
{
    public class OpenAiResponse
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
