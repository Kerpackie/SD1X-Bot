using System;
using System.Threading.Tasks;
using Bot.Common;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace Bot.Modules.Subjects.MYS
{
    public class MYSDelete : SP1XModuleBase
    {
        public MYSDelete(IServiceProvider serviceProvider, IConfiguration configuration) : base(
            serviceProvider,
            configuration)
        {
        }

        [Command("mysdelete", RunMode = RunMode.Async)]
        [RequireUserPermission(GuildPermission.SendMessages)]

        public async Task MYSDeleteCmd(string name)
        {
            var subject = Common.Subjects.MYS;

            var socketGuildUser = Context.User as SocketGuildUser;

            var mysAssignment = await _DataAccessLayer.GetAssignment(subject, name);
            if (mysAssignment == null)
            {
                var embed = new SP1XEmbedBuilder()
                    .WithTitle("Not Found")
                    .WithDescription("The assignment you requested could not be found.")
                    .WithStyle(EmbedStyle.Error)
                    .Build();

                await Context.Channel.SendMessageAsync(embed: embed);
                return;
            }

            if (mysAssignment.OwnerId != Context.User.Id && !socketGuildUser.GuildPermissions.Administrator)
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

            await Context.Channel.SendMessageAsync(embed: deleted);
        }
    }
}
