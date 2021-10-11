using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bot.Common;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace Bot.Modules.Subjects.FOOP
{
    public class FOOPEdit : SP1XModuleBase
    {
        public FOOPEdit(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider,
            configuration)
        {
        }

        [Command("foopedit", RunMode = RunMode.Async)]
        [Alias("oopedit")]
        [RequireUserPermission(GuildPermission.SendMessages)]
        public async Task FOOPEditCmd(string name, string newname, [Remainder] string argument)
        {
            var arguments = argument.Split(" ");
            var subject = "FOOP";

            var socketGuildUser = Context.User as SocketGuildUser;

            var foopAssignment = await _DataAccessLayer.GetAssignment(subject, name);
            if (foopAssignment == null)
            {
                var embed = new SP1XEmbedBuilder()
                    .WithTitle("Not Found")
                    .WithDescription("The FOOP assignment you requested could not be found.")
                    .WithStyle(EmbedStyle.Error)
                    .Build();

                await Context.Channel.SendMessageAsync(embed: embed);
                return;
            }
            if (foopAssignment.OwnerId != Context.User.Id && !socketGuildUser.GuildPermissions.Administrator)
            {
                var embed = new SP1XEmbedBuilder()
                    .WithTitle("Access Denied!")
                    .WithDescription("You need to be a student or administrator to create an FOOP assignment.")
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
