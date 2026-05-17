using System.Text.Json;
using System.Net.Http.Headers;


namespace GenAiApp.api.Services
{
    public class OpenAiServises: IOpenAiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apikey;

        public OpenAiServises(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apikey = configuration["OpenAI:ApiKey"];
        }

        public async Task<string> GenerateFormattedContentAsync(string input, string outputType)
            {
            var prompt = $"Based on the following content , generate a{ outputType} in structured format:\n\n \"{input}\"";
                var request = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                        new { role = "user", content = prompt }
                    },
                    temparature = 0.7,
                    max_tokens = 500
                };
    
                var jsonContent = new StringContent(JsonSerializer.Serialize(request), System.Text.Encoding.UTF8, "application/json");
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _apikey);
    
                var response =
                    await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", jsonContent);
                response.EnsureSuccessStatusCode();
    
                var json = await response.Content.ReadAsStringAsync();
            var root =JsonDocument.Parse(json);
            return root.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
          
        }

    }

}
