namespace GenAiApp.api.Services
{
    public interface IVideoService
    {
        Task<string> ExtractAudioAndTranscribeAsync(string videoPath);
    }
}
