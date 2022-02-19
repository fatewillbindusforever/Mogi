using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.CommandsNext.Entities;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mei.Commands
{
    public class CustomHelpFormatter : BaseHelpFormatter
    {
        protected DiscordEmbedBuilder _embed;
        protected StringBuilder _strBuilder;

        public CustomHelpFormatter(CommandContext ctx) : base(ctx)
        {
            _embed = new DiscordEmbedBuilder();
            _strBuilder = new StringBuilder();
        }

        public override BaseHelpFormatter WithCommand(Command command)
        {
            _embed.AddField("!" + command.Name + " komutu nedir?", command.Description);
            _embed.WithColor(DiscordColor.Rose);
            _strBuilder.AppendLine("!" + $"{command.Name} - {command.Description}");

            return this;
        }

        public override BaseHelpFormatter WithSubcommands(IEnumerable<Command> cmds)
        {
            foreach (var cmd in cmds)
            {
                _embed.AddField("!" + cmd.Name, cmd.Description);
                _embed.WithColor(DiscordColor.Rose);
                _embed.WithTitle("Komutlar");
                _strBuilder.AppendLine($"{cmd.Name} - {cmd.Description}");
            }

            return this;
        }

        public override CommandHelpMessage Build()
        {
            return new CommandHelpMessage(embed: _embed);
            return new CommandHelpMessage(content: _strBuilder.ToString());
        }
    }
}
