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

namespace meichan.Commands
{

    public class Komutlar : BaseCommandModule
    {
        [Command("yaz"), Aliases("write", "wrote", "yazdır", "say"), Description("Bota istediğinizi yazdırır."), RequirePermissions(DSharpPlus.Permissions.Administrator)]
        public async Task GreetCommand(CommandContext ctx, [RemainingText] string name)
        {
            await ctx.Message.DeleteAsync();
            await ctx.Message.Channel.SendMessageAsync($"{name}");
        }

        [Command("avatar"), Description("Kendinizin ya da bir başkasının profil fotoğrafını görmenize yarar.")]
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
    }
}