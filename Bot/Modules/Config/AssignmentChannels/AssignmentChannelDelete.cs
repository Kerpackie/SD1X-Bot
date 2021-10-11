using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bot.Common;
using Discord;
using Discord.Commands;
using Microsoft.Extensions.Configuration;

namespace Bot.Modules.Config.AssignmentChannels
{
    public class AssignmentChannelDelete : SP1XModuleBase
    {
        public AssignmentChannelDelete(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {

        }

        [Command("assignmentchanneldelete")]
        [RequireUserPermission(GuildPermission.Administrator)]

        public async Task AssignmentChannelDeleteCmd(ulong channelId)
        {
            var assignmentChannel = await _DataAccessLayer.GetAssignmentChannel(channelId);
            if (assignmentChannel == null)
            {
                var embed = new SP1XEmbedBuilder()
                    .WithTitle("Not Found")
                    .WithDescription("The assignment channel you requested could not be found.")
                    .WithStyle(EmbedStyle.Error)
                    .Build();

                await Context.Channel.SendMessageAsync(embed: embed);
                return;
            }

            await _DataAccessLayer.DeleteAssignmentChannelUlong(channelId);

            var deleted = new SP1XEmbedBuilder()
                .WithTitle("Assignment Channel Deleted!")
                .WithDescription("The assignment channel was successfully deleted!")
                .WithStyle(EmbedStyle.Success)
                .Build();

            await Context.Channel.SendMessageAsync(embed: deleted);
        }
    }
}
