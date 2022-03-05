using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;

namespace mogi.Commands
{
    public class Komutlar : BaseCommandModule
    {
        [Command("yaz"), Aliases("write", "wrote", "yazdır", "say"), Description("İstediğiniz şeyi bana yazdırmak için kullanabilirsiniz."), RequirePermissions(DSharpPlus.Permissions.Administrator)]
        public async Task Yaz(CommandContext ctx, [RemainingText] string? yazi = null)
        {
            if (yazi != null)
            {
                await ctx.Message.DeleteAsync();
                await ctx.Message.Channel.SendMessageAsync($"{yazi}");
            }
            else
            {
                await ctx.RespondAsync("Yazdırmak istediğiniz yazıyıda yazmalısınız.").ConfigureAwait(false);
            }

        }

        [Command("avatar"), Aliases("pp"), Description("Kendinizin ya da bir başkasının profil fotoğrafına bakmak için kullanabilirsiniz.")]
        public async Task Avatar(CommandContext ctx, DiscordUser? user = null)
        {
            if (user == null)
            {
                var embed = new DiscordEmbedBuilder
                {
                    ImageUrl = $"{ctx.Member.AvatarUrl}",
                    Timestamp = DateTime.UtcNow,
                };
                await ctx.Channel.SendMessageAsync(embed);
            }
            else
            {
                var embed = new DiscordEmbedBuilder
                {
                    Description = $"{user.Username}#{user.Discriminator}",
                    ImageUrl = $"{user.AvatarUrl}",
                    Timestamp = DateTime.UtcNow,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"{ctx.Member.Username}",
                        IconUrl = $"{ctx.Member.AvatarUrl}"
                    },
                };
                await ctx.Channel.SendMessageAsync(embed);
            }
        }

        [Command("kod"), Aliases("github", "opensource"), Description("Kodlarıma ulaşmak için kullanabilirsiniz.")]
        public async Task Kod(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("https://github.com/sinnertenshi/Mogi");
        }

        [Command("sil"), Aliases("temizle", "cc", "clear"), Description("Belirlediğiniz miktarda mesajları silmek için kullanabilirsiniz."), RequirePermissions(DSharpPlus.Permissions.ManageMessages)]
        public async Task Sil(CommandContext ctx, int? amount = null)
        {
            var mesajlar = ctx.Channel.GetMessagesBeforeAsync(ctx.Message.Id, Convert.ToInt32(amount)).Result;
            var silinecek = new List<DiscordMessage>();

            if (amount <= 0) await ctx.Channel.SendMessageAsync("Geçerli bir sayı girmelisiniz.");
            if (amount == null) await ctx.Channel.SendMessageAsync("Kaç adet mesajı sileceğimide yazmanız lazım.");
            if (amount > 100) { await ctx.Channel.SendMessageAsync("Lütfen 100'den fazla mesajı silmeye çalışmayın."); return; }

            foreach (var m in mesajlar)
            {
                if (m.Attachments.Count == 0)
                {
                    silinecek.Add(m);
                }
            }

            await ctx.Channel.DeleteMessagesAsync(silinecek, null).ConfigureAwait(false);
            await ctx.Message.DeleteAsync().ConfigureAwait(false);
            var embed = new DiscordEmbedBuilder
            {
                Description = $"{amount} adet mesajı sildim!",
            };
            await ctx.Channel.SendMessageAsync(embed);
        }
        [Command("ban"), Aliases("yasakla", "banla"), Description("Bir üyeyi sunucudan yasaklamak için kullanabilirsiniz."), RequirePermissions(DSharpPlus.Permissions.BanMembers)]
        public async Task Ban(CommandContext ctx, DiscordUser? user = null, string? reason = null)
        {
            if (user != null && reason != null)
            {
                var embed = new DiscordEmbedBuilder
                {
                    Description = $"{user.Mention}'i yasakladım! ({reason})",
                    Color = DiscordColor.DarkRed,
                };
                await ctx.Channel.SendMessageAsync(embed);
                await ctx.Guild.BanMemberAsync(user.Id, 0, reason);

            }
            else if (user == null)
            {
                await ctx.RespondAsync("Kimi banlayacağımıda yazar mısınız?");
            }
            else if (reason == null)
            {
                await ctx.RespondAsync("Sebebinide yazar mısınız?");
            }
        }
        [Command("unban"), Aliases("bankaldır"), Description("Yasaklı bir üyenin yasağını kaldırmak için kullanabilirsiniz."), RequirePermissions(DSharpPlus.Permissions.BanMembers)]
        public async Task UnBan(CommandContext ctx, DiscordUser? user = null)
        {
            if (user != null)
            {
                var embed = new DiscordEmbedBuilder
                {
                    Description = $"{user.Mention}'in yasağını kaldırdım!",
                    Color = DiscordColor.DarkGreen,
                };
                await ctx.Channel.SendMessageAsync(embed);
                await ctx.Guild.UnbanMemberAsync(user);
            }
        }
    }
}
