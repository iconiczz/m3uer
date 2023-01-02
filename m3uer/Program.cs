using System.CommandLine;

namespace m3uer
{
    class m3uer
    {
        public static async Task<int> Main(string[] args)
        {
            var dirOption = new Option<string>(
            name: "--change-dir",
            description: "Change dir for music path and m3u",
            getDefaultValue: () => Directory.GetCurrentDirectory());

            var formatOption = new Option<string>(
            name: "--format",
            description: "Set the format to be sellected",
            getDefaultValue: () => ".mp3");

            var rootCommand = new RootCommand("M3uer")
            {
                dirOption,
                formatOption,
            };

            rootCommand.SetHandler(async (dir, format) =>
            {
                await M3uer.Start(dir, format);
            }, dirOption, formatOption);

            return await rootCommand.InvokeAsync(args);
        }
    }

    class M3uer
    {
        async public static Task<Array> Start(string dir, string format)
        { 
            string[] FileList = Directory.GetFiles(dir, "*" + format, SearchOption.AllDirectories);

            using FileStream fs = File.Create(dir + "\\Playlist.m3u");
            using (var Writer = new StreamWriter(fs))
            {
                foreach (var item in FileList)
                {
                    Console.WriteLine(item);
                    await Writer.WriteAsync(item + "\n");
                }
            }

            return FileList;
        }
    }
}