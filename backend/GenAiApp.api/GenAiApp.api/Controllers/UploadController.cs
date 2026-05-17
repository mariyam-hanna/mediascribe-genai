using GenAiApp.api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GenAiApp.api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : Controller
    {
        private readonly IVideoService _videoService;
        private readonly IOpenAiService _openAiService;
        private readonly IWhisperService _whisperService;

        public UploadController(IVideoService videoService, IOpenAiService openAiService, IWhisperService whisperService)
        {
            _videoService = videoService;
            _openAiService = openAiService;
            _whisperService = whisperService;
        }

        [HttpPost(Name = "UploadFiles")]
        public async Task<IActionResult> Upload([FromForm]IFormFile file, [FromForm]string outputType)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            var filePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}{extension}");
            using (var stream = new FileStream(filePath,FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            string extractedText = extension switch
            {
                ".txt" => await System.IO.File.ReadAllTextAsync(filePath),
                ".mp3" or ".wav" => await _whisperService.TranscribeAudioAsync(filePath),
                ".mp4" or ".avi" => await _videoService.ExtractAudioAndTranscribeAsync(filePath),
                _ => throw new NotSupportedException("Unsupported file type.")
            };
            var content = await _openAiService.GenerateFormattedContentAsync(extractedText, outputType);
            return Ok(new { content });
        }
    }
}
