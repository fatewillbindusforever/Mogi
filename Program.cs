using System;
using System.IO;
using System.Timers;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using mogi.Commands;

namespace mogi
{
    class Program
    {
        static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
        }
        static async Task MainAsync()
        {
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = "bottoken",
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                UseRelativeRatelimit = true,
                Intents = DiscordIntents.All
            });

            discord.GuildDownloadCompleted += GuildDownloadCompleted;
            discord.GuildMemberAdded += GuildMemberAdded;
            discord.MessageCreated += MessageCreated;
            discord.MessageUpdated += MessageUpdated;

            var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { "!" }
            });

            commands.RegisterCommands<Komutlar>();
            commands.SetHelpFormatter<CustomHelpFormatter>();

            await discord.ConnectAsync();
            Console.WriteLine("Mogi burada!");
            await Task.Delay(-1);
        }

        private static async Task MessageCreated(DiscordClient s, MessageCreateEventArgs e)
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
                await e.Message.RespondAsync("Aleyküm Selam.");
            }
            else if (e.Message.Content.ToLower() == "selamun aleyküm")
            {
                await e.Message.RespondAsync("Aleyküm Selam!");
            }
            else if (e.Message.Content.ToLower() == "selamun aleykum")
            {
                await e.Message.RespondAsync("Aleyküm Selam.");
            }
            else if (e.Message.Content.ToLower() == "ping")
            {
                await e.Message.RespondAsync("pong!");
            }
            else if (e.Message.Content.ToLower() == "nbr")
            {
                await e.Message.RespondAsync("İyi senden?");
            }
            else if (e.Message.Content.ToLower() == "sea")
            {
                await e.Message.RespondAsync("Aleyküm Selam.");
            }
        }
        private static async Task GuildMemberAdded(DiscordClient s, GuildMemberAddEventArgs e)
        {
            var kanal = await s.GetChannelAsync(channelid);
            var embed = new DiscordEmbedBuilder
            {
                Description = $"{e.Member.Mention} aramıza katıldı!",
            };
            await kanal.SendMessageAsync(embed);
        }
        private static async Task GuildDownloadCompleted(DiscordClient s, GuildDownloadCompletedEventArgs e)
        {
            await s.UpdateStatusAsync(new DiscordActivity("Geliştiriliyor", ActivityType.Watching), UserStatus.Idle);
        }
    }
}
