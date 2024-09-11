using YoutubeExplode;
using YoutubeExplode.Converter;

namespace Module_18_Patterns_Final
{
    // https://www.youtube.com/watch?v=tOVBru1gDVo
    // https://www.youtube.com/watch?v=c9DIoSNoQNs
    //паттерн команда (Command) и библиотеку YoutubeExplode
    //Позволяет нам превратить запросы в объекты и передавать их потом как аргументы при вызове методов.

    public class Program
    {
        static async Task Main(string[] args)
        {
            string videoUrl;
            
            var outputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "D:\\Downloads");
            Directory.CreateDirectory(outputDirectory);
            var ffmpegPath = "D:\\Downloads\\ffmpeg-master-latest-win64-gpl-shared\\ffmpeg-master-latest-win64-gpl-shared\\bin\\ffmpeg.exe";

            if (args.Length == 0)
            {
                Console.WriteLine("Please provide a Youtube URL");
                videoUrl = Console.ReadLine();
            }
            else 
            { 
                videoUrl = args[0];
            }

            if (string.IsNullOrWhiteSpace(videoUrl))
            {
                Console.WriteLine("No valid URL provided. Exiting program...");
                return;
            }

            bool continueRunning = true;

            while (continueRunning)
            {
                Console.WriteLine("Choose an option: ");
                Console.WriteLine("1. Get video info.");
                Console.WriteLine("2. Download video.");
                Console.WriteLine("3. Exit.");

                var userInput = Console.ReadLine();

                ICommand command;

                switch (userInput)
                {
                    case "1":
                        Console.WriteLine("Getting the video info: ");
                        command = new GetVideoInfoCommand(videoUrl);
                        await command.ExecuteAsync();
                        break;
                    case "2":
                        Console.WriteLine("Starting download...");
                        command = new DownloadVideoCommand(videoUrl, outputDirectory, ffmpegPath);
                        await command.ExecuteAsync();
                        break;
                    case "3":
                        Console.WriteLine("Exit programm.");
                        continueRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            }
        }
    }  
}
