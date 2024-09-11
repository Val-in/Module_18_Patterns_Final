using YoutubeExplode;
using YoutubeExplode.Converter;

namespace Module_18_Patterns_Final
{
    public class DownloadVideoCommand : ICommand
    {
        public readonly string _videoUrl;
        public readonly string _outputFilePath;
        private readonly string _ffmpegPath;

        public DownloadVideoCommand(string videoUrl, string outputFilePath, string ffmpegPath)
        {
            _videoUrl = videoUrl;
            _outputFilePath = outputFilePath;
            _ffmpegPath = ffmpegPath;
        }

        public async Task ExecuteAsync()
        {
            try
            {
                var youtube = new YoutubeClient();

                // Получаем информацию о видео
                var video = await youtube.Videos.GetAsync(_videoUrl);

                // Создаем название файла, удаляя неразрешенные символы
                var sanitizedTitle = string.Join("_", video.Title.Split(Path.GetInvalidFileNameChars()));
                var outputFilePath = Path.Combine(_outputFilePath, sanitizedTitle + ".mp4");

                // Скачиваем видео
                await youtube.Videos.DownloadAsync(_videoUrl, outputFilePath, builder =>
                    builder.SetPreset(ConversionPreset.UltraFast)
                           .SetFFmpegPath(_ffmpegPath));

                Console.WriteLine("Video downloaded successfully!");
                Console.WriteLine($"The video has been saved to: {outputFilePath}"); // Показываем путь к файлу
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while downloading the video: {ex.Message}");
            }
        }

    }
}
