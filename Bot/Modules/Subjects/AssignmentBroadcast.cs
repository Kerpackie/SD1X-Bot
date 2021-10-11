using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Bot.Common;
using Bot.Utilities;
using Discord;
using Discord.Commands;
using Microsoft.Extensions.Configuration;

namespace Bot.Modules.Subjects
{
    public class AssignmentBroadcast : SP1XModuleBase
    {
        private readonly AssignmentControl _assignmentControl;
        public AssignmentBroadcast(IServiceProvider serviceProvider, IConfiguration configuration, AssignmentControl assignmentControl) : base(serviceProvider,
            configuration)
        {
            _assignmentControl = assignmentControl;
        }

        [Command("assignmentbroadcast", RunMode = RunMode.Async)]
        [Alias("assbroadcast", "assbeam")]
        [RequireUserPermission(GuildPermission.Administrator)]

        public async Task AssignmentBroadcastCmd(int id)
        {
            var assignment = await _DataAccessLayer.GetAssignmentId(id);
            if (assignment is null)
            {
                var embed = new SP1XEmbedBuilder()
                    .WithTitle("Not Found")
                    .WithDescription("The assignment you requested could not be found.")
                    .WithStyle(EmbedStyle.Error)
                    .Build();

                await Context.Channel.SendMessageAsync(embed: embed);
                return;
            }

            var confirmationEmbed = new SP1XEmbedBuilder()
                .WithTitle($"New assignment being broadcast:{assignment.Subject} {assignment.Name}")
                .WithDescription($"Assignment being broadcast with content: {assignment.Content}")
                .WithFooter($"Broadcast sent by: {Context.User.Id}")
                .WithStyle(EmbedStyle.Success)
                .Build();

            await Context.Channel.SendMessageAsync(embed: confirmationEmbed);
            await _assignmentControl.SendAssignmentAsync(Context.Guild,
                $"New assignment in: {assignment.Subject}, with the name {assignment.Name}", $"{assignment.Content}");
        }
    }
}
