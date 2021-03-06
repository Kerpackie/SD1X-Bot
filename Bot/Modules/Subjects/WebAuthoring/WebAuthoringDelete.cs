using Bot.Common;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Bot.Modules.Subjects.WebAuthoring
{
    public class WebAuthoringDelete : SP1XModuleBase
    {
        public WebAuthoringDelete(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider,
            configuration)
        {
        }

        [Command("webauthoringdelete", RunMode = RunMode.Async)]
        [Alias("webauthdelete")]
        [RequireUserPermission(GuildPermission.SendMessages)]

        public async Task WordProcessingDeleteCmd(string name)
        {
            var subject = Common.Subjects.WebAuthoring;

            var socketGuildUser = Context.User as SocketGuildUser;

            var foopAssignment = await _DataAccessLayer.GetAssignment(subject, name);
            if (foopAssignment == null)
            {
                var embed = new SP1XEmbedBuilder()
                    .WithTitle("Not Found")
                    .WithDescription("The assignment you requested could not be found.")
                    .WithStyle(EmbedStyle.Error)
                    .Build();

                await Context.Channel.SendMessageAsync(embed: embed);
                return;
            }
            if (foopAssignment.OwnerId != Context.User.Id && !socketGuildUser.GuildPermissions.Administrator)
            {
                var embed = new SP1XEmbedBuilder()
                    .WithTitle("Access Denied!")
                    .WithDescription("You need to be a student or administrator to create an assignment.")
                    .WithStyle(EmbedStyle.Error)
                    .Build();

                await Context.Channel.SendMessageAsync(embed: embed);
                return;
            }

            await _DataAccessLayer.DeleteAssignmentWithSubjectName(subject, name);

            var deleted = new SP1XEmbedBuilder()
                .WithTitle("Assignment Deleted!")
                .WithDescription("The assignment was successfully deleted.")
                .WithStyle(EmbedStyle.Success)
                .Build();

            await this.Context.Channel.SendMessageAsync(embed: deleted);
        }
    }
}

