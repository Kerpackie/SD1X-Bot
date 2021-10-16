﻿using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Bot.Common;
using Discord;
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
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task AssignmentChannelCreateCmd(ulong channelId)
        {
            var socketGuildUser = Context.User as SocketGuildUser;
            var newChannel = await _DataAccessLayer.GetAssignmentChannel(channelId);

            if (newChannel == null)
            {
                var guildId = Context.Guild.Id;
                await _DataAccessLayer.CreateAssignmentChannel(channelId, guildId);

                var embed = new SP1XEmbedBuilder()
                    .WithTitle("New Assignment Channel Added!")
                    .WithDescription("A new assignment channel has been added successfully.")
                    .WithStyle(EmbedStyle.Success)
                    .Build();
                await Context.Channel.SendMessageAsync(embed: embed);
                return;
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
