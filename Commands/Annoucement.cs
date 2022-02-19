using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace mei.Commands
{

    public class Announcements : BaseCommandModule
    {   
        [Command("yaz"), Aliases("write","wrote","yazdır","say"), Description("Bota istediğinizi yazdırır."), RequirePermissions(DSharpPlus.Permissions.Administrator)]
        public async Task GreetCommand(CommandContext ctx, [RemainingText] string name)
        {
            await ctx.Message.DeleteAsync();
            await ctx.Message.Channel.SendMessageAsync($"{name}");
        }
    }
}