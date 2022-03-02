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
        [Command("yaz"), Aliases("write", "wrote", "yazdır", "say"), Description("Bota istediğinizi yazdırır."), RequirePermissions(DSharpPlus.Permissions.Administrator)]
        public async Task GreetCommand(CommandContext ctx, [RemainingText] string name = null)
        {
            if (name != null)
            {
                await ctx.Message.DeleteAsync();
                await ctx.Message.Channel.SendMessageAsync($"{name}");
            }
            else
            {
                await ctx.RespondAsync("Yazabilmem için bir şeyler söyleeee!").ConfigureAwait(false);
            }

        }
        
        [Command("avatar"), Aliases("pp"), Description("Kendinizin ya da bir başkasının profil fotoğrafını görmenize yarar.")]
        public async Task Avatar(CommandContext ctx, DiscordUser user = null)
        {
            if (user == null)
            {
                var embed = new DiscordEmbedBuilder
                {
                    ImageUrl = $"{ctx.Member.AvatarUrl}",
                    Timestamp = DateTime.UtcNow,
                };
                var msg = await ctx.Channel.SendMessageAsync(embed);
            }
            else
            {
                var embed = new DiscordEmbedBuilder
                {
                    ImageUrl = $"{user.AvatarUrl}",
                    Timestamp = DateTime.UtcNow,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"{ctx.Member.Username}",
                        IconUrl = $"{ctx.Member.AvatarUrl}"
                    },
                };
                var msg = await ctx.Channel.SendMessageAsync(embed);
            }
        }
        
        [Command("kod"), Aliases("github"), Description("Botun kaynak kodlarına ulaşmak için kullanılır.")]
        public async Task Kod(CommandContext ctx, DiscordUser user = null)
        {
            await ctx.Channel.SendMessageAsync("https://github.com/sinnertenshi/Mogi");
        }
        
        [Command("sil"), Aliases("temizle", "cc", "clear"), Description("Belirlediğiniz miktarda mesajları siler."), RequirePermissions(DSharpPlus.Permissions.ManageMessages)]
        public async Task Sil(CommandContext ctx, int? amount = null)
        {
            var mesajlar = ctx.Channel.GetMessagesBeforeAsync(ctx.Message.Id, Convert.ToInt32(amount)).Result;
            var silinecek = new List<DiscordMessage>();

            if (amount <= 0) await ctx.Channel.SendMessageAsync("Olmayan mesaj nasıl silinebilir ki??");
            if (amount == null) await ctx.Channel.SendMessageAsync("En azından kaç adet mesaj sileceğimi söyleeee!");
            if (amount > 100) { await ctx.Channel.SendMessageAsync("Bu kadar mesajı silmek beni uğraştırır!"); return; }

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
                Timestamp = DateTime.UtcNow,
            };
            await ctx.Channel.SendMessageAsync(embed);
        }
    }
}
