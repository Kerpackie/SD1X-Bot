using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bot.Common;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace Bot.Modules.Subjects.SoftwareArchitecture
{
    public class SoftwareArchitectureDelete : SP1XModuleBase
    {
        public SoftwareArchitectureDelete(IServiceProvider serviceProvider, IConfiguration configuration) : base(
            serviceProvider,
            configuration)
        {
        }

        [Command("softwarearchdelete", RunMode = RunMode.Async)]
        [Alias("softwaredelete")]

        public async Task SoftwareArchitectureDeleteCmd(string name, [Remainder] string argument)
        {
            var subject = "Software Architecture";

            var socketGuildUser = Context.User as SocketGuildUser;

            var softwareAssignment = await _DataAccessLayer.GetAssignment(subject, name);
            if (softwareAssignment == null)
            {
                var embed = new SP1XEmbedBuilder()
                    .WithTitle("Not Found")
                    .WithDescription("The assignment you requested could not be found.")
                    .WithStyle(EmbedStyle.Error)
                    .Build();

                await Context.Channel.SendMessageAsync(embed: embed);
                return;
            }

            if (softwareAssignment.OwnerId != Context.User.Id && !socketGuildUser.GuildPermissions.Administrator)
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
