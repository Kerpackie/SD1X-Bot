using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bot.Common;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace Bot.Modules.Subjects.Communications
{
    public class Communications : SP1XModuleBase
    {
        public Communications(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider,
            configuration)
        {
        }

        [Command("communications", RunMode = RunMode.Async)]
        [Alias("comms")]

        public async Task CommunicationsCmd([Remainder] string argument)
        {
            var arguments = argument.Split(" ");
            var subject = "Communications";

            if (arguments.Count() == 1 && arguments[0] != "create")
            {
                var commsAssignment = await _DataAccessLayer.GetAssignmentSubject(subject, arguments[0]);
                if (commsAssignment is null)
                {
                    var embed = new SP1XEmbedBuilder()
                        .WithTitle("Not Found")
                        .WithDescription("The assignment you requested could not be found.")
                        .WithStyle(EmbedStyle.Error)
                        .Build();

                    await Context.Channel.SendMessageAsync(embed: embed);
                    return;
                }

                var foopEmbed = new SP1XEmbedBuilder()
                    .WithTitle(commsAssignment.Name)
                    .WithDescription(commsAssignment.Content)
                    .WithStyle(EmbedStyle.Information)
                    .Build();

                await Context.Channel.SendMessageAsync(embed: foopEmbed);
                return;
            }

            var socketGuildUser = Context.User as SocketGuildUser;

            switch (arguments[0])
            {
                case "create":
                    var assignment = await _DataAccessLayer.GetAssignment(subject, arguments[1]);
                    if (assignment != null)
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Already Exists")
                            .WithDescription("There already exists an assignment with that name.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await Context.Channel.SendMessageAsync(embed: embed);
                        return;
                    }

                    if (!Context.User.IsPromoted())
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Access Denied!")
                            .WithDescription("You need to be a student or administrator to create an assignment.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await Context.Channel.SendMessageAsync(embed: embed);
                        return;
                    }

                    await _DataAccessLayer.CreateAssignment(subject, Context.User.Id, arguments[1],
                        string.Join(" ", arguments.Skip(2)));

                    var prefix = "!";

                    var created = new SP1XEmbedBuilder()
                        .WithTitle("Assignment Created!")
                        .WithDescription(
                            $"The Assignment has been successfully created. You can view it by using `{prefix}comms {arguments[1]}`")
                        .WithStyle(EmbedStyle.Success)
                        .Build();
                    await Context.Channel.SendMessageAsync(embed: created);
                    break;
            }
        }
    }
}
