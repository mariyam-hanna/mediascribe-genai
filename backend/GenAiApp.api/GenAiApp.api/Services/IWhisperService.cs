namespace GenAiApp.api.Services
{
    public interface IWhisperService
    {
        Task<string> TranscribeAudioAsync(string filePath);

    }
}
