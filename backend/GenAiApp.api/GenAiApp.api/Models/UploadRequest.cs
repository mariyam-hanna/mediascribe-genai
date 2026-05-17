namespace GenAiApp.api.Models
{
    public class UploadRequest
    {
        public IFormFile File { get; set; }
        public string OutputType { get; set; }
    }
}
