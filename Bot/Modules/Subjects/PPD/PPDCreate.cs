using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bot.Common;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace Bot.Modules.Subjects.PPD
{
    public class PPDCreate : SP1XModuleBase
    {
        public PPDCreate(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider,
            configuration)
        {
        }

        [Command("ppdcreate", RunMode = RunMode.Async)]
        [RequireUserPermission(GuildPermission.SendMessages)]

        public async Task PPDCreateCmd([Remainder] string argument)
        {
            var arguments = argument.Split(" ");
            var subject = Common.Subjects.PPD;

            var ppdAssignment = await _DataAccessLayer.GetAssignment(subject, arguments[0]);
            if (ppdAssignment != null)
            {
                var embed = new SP1XEmbedBuilder()
                    .WithTitle("Already Exists")
                    .WithDescription("The assignment you requested already exists.")
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
                    $"The Assignment has been successfully created. You can view it by using `{prefix}ppd {arguments[0]}`")
                .WithStyle(EmbedStyle.Success)
                .Build();
            await Context.Channel.SendMessageAsync(embed: created);
        }
    }
}
