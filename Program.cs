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
                StringPrefixes = new[] { "!" } // prefixi buradan değiştirilebilir.
            });

            commands.RegisterCommands < Write > ();


            discord.MessageCreated += async (s, e) =>
            {
                if (e.Message.Content.ToLower() == "selam")
                {
                    await e.Message.RespondAsync("Selam!");
                }
                else if(e.Message.Content.ToLower() == "ping")
                {
                    await e.Message.RespondAsync("pong!");
                }
            };

            await discord.ConnectAsync();
	    Console.WriteLine("Mei burada!");
            await Task.Delay(-1);
         }
    }
}