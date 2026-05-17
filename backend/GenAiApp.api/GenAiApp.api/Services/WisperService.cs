using GenAiApp.api.Models;
using System.Text.Json;
using System.Net.Http.Headers;

namespace GenAiApp.api.Services
{
    public class WhisperService : IWhisperService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apikey;

        public WhisperService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apikey = configuration["OpenAI:ApiKey"];
        }
        public async Task<string> TranscribeAudioAsync(string filePath)
        {
            using var multipart = new MultipartFormDataContent();
            var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(filePath));
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("audio/mpeg");
            multipart.Add(fileContent,"file",Path.GetFileName(filePath));
            multipart.Add(new StringContent("whisper-1"), "model");

            _httpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", _apikey);

           var response =
                await _httpClient.PostAsync("https://api.openai.com/v1/audio/transcriptions", multipart);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<OpenAiResponse>(json)?.Text;
        }
    }
}
