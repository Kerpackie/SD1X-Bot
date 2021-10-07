using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bot.Common;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace Bot.Modules.Subjects.PPD
{
    public class PPDEdit : SP1XModuleBase
    {
        public PPDEdit(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("ppdedit", RunMode = RunMode.Async)]
        public async Task PPDEditCmd(string name, string newname, [Remainder] string argument)
        {
            var arguments = argument.Split(" ");
            var subject = "PPD";

            var socketGuildUser = Context.User as SocketGuildUser;

            var ppdAssignment = await _DataAccessLayer.GetAssignment(subject, name);
            if (ppdAssignment == null)
            {
                var embed = new SP1XEmbedBuilder()
                    .WithTitle("Not Found")
                    .WithDescription("The assignment you requested could not be found.")
                    .WithStyle(EmbedStyle.Error)
                    .Build();

                await Context.Channel.SendMessageAsync(embed: embed);
                return;
            }
            if (ppdAssignment.OwnerId != Context.User.Id && !socketGuildUser.GuildPermissions.Administrator)
            {
                var embed = new SP1XEmbedBuilder()
                    .WithTitle("Access Denied!")
                    .WithDescription("You need to be a student or administrator to create an assignment.")
                    .WithStyle(EmbedStyle.Error)
                    .Build();

                await Context.Channel.SendMessageAsync(embed: embed);
                return;
            }

            await _DataAccessLayer.EditAssignmentContentSubject(subject, name, newname, argument);

            var edited = new SP1XEmbedBuilder()
                .WithTitle($"You have successfully edited the assignment {name}")
                .WithDescription($"Assignment {name} now renamed to {newname}. \n \n Assignment Content changed to: \n {argument}")
                .WithStyle(EmbedStyle.Error)
                .Build();
            await Context.Channel.SendMessageAsync(embed: edited);
        }
    }
}
