using YoutubeExplode;

namespace Module_18_Patterns_Final
{
    public class GetVideoInfoCommand : ICommand
    {
        private readonly string _videoUrl;

        public GetVideoInfoCommand(string videoUrl)
        {
            _videoUrl = videoUrl;
        }

        public async Task ExecuteAsync()
        {
            var youTube = new YoutubeClient();
            var video = await youTube.Videos.GetAsync(_videoUrl);

            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Description: {video.Description}");
        }
    }
}
