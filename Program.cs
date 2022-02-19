using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
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
                Token = "bottokeni",
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                UseRelativeRatelimit = true,
                Intents = DiscordIntents.All
            });

            discord.GuildDownloadCompleted += GuildDownloadCompleted;

            var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { "!" }
            });

            commands.RegisterCommands<Komutlar>();
            commands.SetHelpFormatter<CustomHelpFormatter>();

            discord.GuildMemberAdded += (s, e) =>
            {
                _ = Task.Run(async () =>
                {
                    var kanal = await s.GetChannelAsync(kanalid);
                    var embed = new DiscordEmbedBuilder
                    {
                        Description = $"{e.Member.Mention} aramıza katıldı!",
                    };
                    var msg = await kanal.SendMessageAsync(embed);
                    Console.WriteLine($"{e.Member.DisplayName}, sunucuya giriş yaptı. {DateTime.UtcNow}");
                });
                return Task.CompletedTask;
            };

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

                string msg = e.Message.Content.ToLower();
                List<string> BadWords = new List<string>()
                {
                    "sikerim",
                    "siktir",
                    "sikecem",
                    "ibne",
                    "amına",
                    "orospu",
                    "gavat",
                    "kahpe",
                    "piç",
                    "fuck",
                    "motherfucker",
                    "porno",
                    "porn",
                    "sikiş",
                    "sikişelim",
                    "sikik",
                    "sikis",
                    "göt",
                };
                if (BadWords.Any(x => msg.Contains(x)))
                {
                    await e.Message.DeleteAsync();
                    await e.Message.Channel.SendMessageAsync("Şşş küfür yok!");
                }
            };

            await discord.ConnectAsync();
            Console.WriteLine("Mei burada!");
            await Task.Delay(-1);
        }

        private static async Task GuildDownloadCompleted(DiscordClient sender, GuildDownloadCompletedEventArgs e)
        {
            await sender.UpdateStatusAsync(new DiscordActivity("Geliştiriliyor", ActivityType.Watching), UserStatus.Idle);
        }
    }
}
