using System;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using mei.Commands;

namespace mei
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }
        static async Task MainAsync()
        {
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = "type your token",
                TokenType = TokenType.Bot,
                AutoReconnect = true,
            });

           
            var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { "!" } 
            });

            commands.RegisterCommands < Write > ();


            discord.MessageCreated += async (s, e) =>
            {
                if (e.Message.Content.ToLower() == "selam")
                {
                    await e.Message.RespondAsync("Selam!");
                }
                else if (e.Message.Content.ToLower() == "slm")
                {
                    await e.Message.RespondAsync("Selam!");
                }
                else if (e.Message.Content.ToLower() == "naber")
                {
                    await e.Message.RespondAsync("İyi senden?");
                }
                else if (e.Message.Content.ToLower() == "sa")
                {
                    await e.Message.RespondAsync("Aleyküm Selam");
                }
                else if (e.Message.Content.ToLower() == "selamun aleyküm")
                {
                    await e.Message.RespondAsync("Aleyküm Selam!");
                }
                else if (e.Message.Content.ToLower() == "selamun aleykum")
                {
                    await e.Message.RespondAsync("Aleyküm Selam");
                }
                else if (e.Message.Content.ToLower() == "ping")
                {
                    await e.Message.RespondAsync("pong!");
                }
                else if (e.Message.Content.ToLower() == "nbr")
                {
                    await e.Message.RespondAsync("İyi senden?");
                }
            };

            await discord.ConnectAsync();
	    Console.WriteLine("Mei burada!");
            await Task.Delay(-1);
         }
    }
}