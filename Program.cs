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
                Token = "TOKEN",
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                UseRelativeRatelimit = true,
                Intents = DiscordIntents.All
            });

            discord.GuildDownloadCompleted += GuildDownloadCompleted;
            discord.MessageCreated += MessageCreated;

            var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { "!" }
            });

            commands.RegisterCommands<Komutlar>();
            commands.SetHelpFormatter<CustomHelpFormatter>();

            discord.GuildMemberAdded += (s, e) =>
            {
                if(e.Guild.Id == guildid)
                {
                    DiscordRole gRole = e.Guild.GetRole(roleid);
                    e.Member.GrantRoleAsync(gRole);
                }
                else if(e.Guild.Id == guildid)
                {
                    DiscordRole gRole = e.Guild.GetRole(roleid);
                    e.Member.GrantRoleAsync(gRole);

                    var wChannel = e.Guild.GetChannel(channelid);
                    wChannel.SendMessageAsync($"{e.Member.Mention} geldiğin için teşekkürler!");
                }

                return Task.CompletedTask;
            };

            await discord.ConnectAsync();
            Console.WriteLine("Mogi burada!");
            await Task.Delay(-1);
        }
        private static async Task MessageCreated(DiscordClient s, MessageCreateEventArgs e)
        {
            if (!e.Message.Author.IsBot)
            {
                if (e.Message.Content.ToLower() == "selam") await e.Message.RespondAsync("Selam!");
                else if (e.Message.Content.ToLower() == "mogi")
                {
                    string[] mMsg = {
                    "Merhaba, bana mı seslendiniz?",
                    "Size nasıl yardımcı olabilirim?",
                    "Yardıma ihtiyacınız varsa !help komutunu kullanabilirsiniz."
                    };
                    
                    Random rand = new Random();
                    int send = rand.Next(mMsg.Length);
                    await e.Channel.SendMessageAsync($"{mMsg[send]}");
                }
            }
        }
        private static async Task GuildDownloadCompleted(DiscordClient s, GuildDownloadCompletedEventArgs e)
        {
            await s.UpdateStatusAsync(new DiscordActivity("Geliştiriliyor", ActivityType.Watching), UserStatus.Idle);
        }
    }
}
