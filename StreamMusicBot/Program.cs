using System;
using System.Threading.Tasks;

namespace StreamMusicBot
{
    class Program
    {
        static async Task Main(string[] args)
            => await new StreamMusicBotClient().InitializeAsync();
    }
}
