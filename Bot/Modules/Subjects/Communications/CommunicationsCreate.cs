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

namespace Bot.Modules.Subjects.Communications
{
    public class CommunicationsCreate : SP1XModuleBase
    {
        public CommunicationsCreate(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider,
            configuration)
        {
        }

        [Command("communicationscreate", RunMode = RunMode.Async)]
        [Alias("commscreate")]
        [RequireUserPermission(GuildPermission.SendMessages)]

        public async Task CommunicationsCreateCmd([Remainder] string argument)
        {
            var arguments = argument.Split(" ");
            var subject = "Communications";

            var commsAssignment = await _DataAccessLayer.GetAssignment(subject, arguments[0]);
            if (commsAssignment != null)
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
                .WithDescription($"The Assignment has been successfully created. You can view it by using `{prefix}comms {arguments[0]}`")
                .WithStyle(EmbedStyle.Success)
                .Build();
            await Context.Channel.SendMessageAsync(embed: created);
        }
    }
}
