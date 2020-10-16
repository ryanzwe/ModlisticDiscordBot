using Discord;
using Discord.WebSocket;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace ModlisticDiscordBot
{
    class Program
    {
        private DiscordSocketClient client;
        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();




        public async Task MainAsync()
        {
            client = new DiscordSocketClient();
            client.Log += Log;

            var token = "";
            if (File.Exists("BotToken.txt")) token = File.ReadAllText("BotToken.txt");
            else
            {
                Console.WriteLine("\n\n\n\nFILE NOT FOUND\n\n\n\n");
                return;
            }
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();
            //Block this until program closes
            await Task.Delay(-1);
        }
        private Task Log(LogMessage msg)
        {
            Console.WriteLine($"Modlistic: {msg.ToString()}");
            return Task.CompletedTask;
        }

    }

}
