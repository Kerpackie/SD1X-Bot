using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bot.Common;
using Discord.Commands;
using Discord.WebSocket;

namespace Bot.Modules.Config.AssignmentChannels
{
    public class AssignmentChannelCreate : SP1XModuleBase
    {
        public AssignmentChannelCreate(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {

        }

        [Command("assignmentchannelcreate")]
        public async Task AssignmentChannelCreateCmd(ulong channelId)
        {
            var socketGuildUser = Context.User as SocketGuildUser;
            var newChannel = await _DataAccessLayer.GetAssignmentChannel(channelId);

            if (newChannel != null)
            {
                await _DataAccessLayer.CreateAssignmentChannel(channelId);

                var embed = new SP1XEmbedBuilder()
                    .WithTitle("New Assignment Channel Added!")
                    .WithDescription("A new assignment channel has been added successfully.")
                    .WithStyle(EmbedStyle.Success)
                    .Build();
                await Context.Channel.SendMessageAsync(embed: embed);
            }

            var exists = new SP1XEmbedBuilder()
                .WithTitle("Error: Assignment Channel already exists")
                .WithDescription("This channel already exists, so we will not add it a second time.")
                .WithStyle(EmbedStyle.Error)
                .Build();

            await Context.Channel.SendMessageAsync(embed: exists);
        }
    }
}
