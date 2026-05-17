namespace GenAiApp.api.Services
{
    public interface IOpenAiService
    {
        Task<string> GenerateFormattedContentAsync(string input, string outputType);
    }
}
