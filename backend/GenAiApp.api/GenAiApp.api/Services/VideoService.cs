
using Xabe.FFmpeg;

namespace GenAiApp.api.Services
{
    public class VideoService : IVideoService
    {
        private readonly IWhisperService _whisperService;
        public VideoService(IWhisperService whisperService)
        {
            _whisperService = whisperService;
            FFmpeg.SetExecutablesPath("ffmpeg_bin_path");
        }



        public async Task<string> ExtractAudioAndTranscribeAsync(string videoPath)
        {
            var audioPath = Path.ChangeExtension(videoPath, ".mp3");
            var conversation = await FFmpeg.Conversions.FromSnippet.ExtractAudio(videoPath, audioPath);
            await conversation.Start();
            return await _whisperService.TranscribeAudioAsync(audioPath);
        }
    }
}
