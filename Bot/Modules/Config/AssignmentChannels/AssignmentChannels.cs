using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bot.Common;
using Bot.Modules.Notes;
using Discord;
using Discord.Commands;
using Microsoft.Extensions.Configuration;

namespace Bot.Modules.Config.AssignmentChannels
{
    public class AssignmentChannels : SP1XModuleBase
    {
        public AssignmentChannels(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("assignmentchannels", RunMode = RunMode.Async)]
        [RequireUserPermission(GuildPermission.Administrator)]

        public async Task GetAssignmentChannels()
        {
            var assignmentChannels = await _DataAccessLayer.GetAssignmentChannels(Context.Guild.Id);

            if (!assignmentChannels.Any())
            {
                var noChannels = new SP1XEmbedBuilder()
                    .WithTitle("No Assignment Channels Found")
                    .WithDescription("This server does not have any Assignment Channels created yet.")
                    .WithStyle(EmbedStyle.Error)
                    .Build();

                await Context.Channel.SendMessageAsync(embed: noChannels);
                return;
            }

            string description = string.Join("\n", assignmentChannels.Select(x => x.Channel));
            var prefix = "!";

            var list = new SP1XEmbedBuilder()
                .WithTitle($"Assignment Channels: ({assignmentChannels.Count()})")
                .WithDescription(description)
                .WithFooter($"Command run by: {Context.User.Id}")
                .Build();
            await Context.Channel.SendMessageAsync(embed: list);
        }
    }
}
