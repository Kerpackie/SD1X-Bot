using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bot.Common;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace Bot.Modules.Subjects.MathForIT
{
    public class MathForITCreate : SP1XModuleBase
    {
        public MathForITCreate(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider,
            configuration)
        {
        }

        [Command("mathcreate", RunMode = RunMode.Async)]

        public async Task MathCreateCmd(string name, [Remainder] string argument)
        {
            var arguments = argument.Split(" ");
            var subject = "Math for IT";

            var socketGuildUser = Context.User as SocketGuildUser;

            var mathAssignment = await _DataAccessLayer.GetAssignment(subject, name);
            if (mathAssignment == null)
            {
                var embed = new SP1XEmbedBuilder()
                    .WithTitle("Not Found")
                    .WithDescription("The assignment you requested could not be found.")
                    .WithStyle(EmbedStyle.Error)
                    .Build();

                await Context.Channel.SendMessageAsync(embed: embed);
                return;
            }

            if (mathAssignment.OwnerId != Context.User.Id && !socketGuildUser.GuildPermissions.Administrator)
            {
                var embed = new SP1XEmbedBuilder()
                    .WithTitle("Access Denied!")
                    .WithDescription("You need to be a student or administrator to create an assignment.")
                    .WithStyle(EmbedStyle.Error)
                    .Build();

                await Context.Channel.SendMessageAsync(embed: embed);
                return;
            }

            await _DataAccessLayer.CreateAssignment(subject, Context.User.Id, arguments[0],
                string.Join(" ", arguments.Skip(1)));

            var prefix = "!";

            var created = new SP1XEmbedBuilder()
                .WithTitle("Assignment Created!")
                .WithDescription(
                    $"The Assignment has been successfully created. You can view it by using `{prefix}math {arguments[1]}`")
                .WithStyle(EmbedStyle.Success)
                .Build();
            await Context.Channel.SendMessageAsync(embed: created);
        }
    }
}
