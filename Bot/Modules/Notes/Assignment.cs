using System;
using System.Linq;
using System.Threading.Tasks;
using Bot.Common;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace Bot.Modules.Notes
{
    public class Assignment : SP1XModuleBase
    {
        public Assignment(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
            
        }

        [Command("assignments", RunMode = RunMode.Async)]
        public async Task Assignments()
        {
            var assignments = await _DataAccessLayer.GetAssignments();

            if (!assignments.Any())
            {
                var noAssignments = new SP1XEmbedBuilder()
                    .WithTitle("No Assignments Found")
                    .WithDescription("This server does not have any assignments yet.")
                    .WithStyle(EmbedStyle.Error)
                    .Build();

                await Context.Channel.SendMessageAsync(embed: noAssignments);
                return;
            }

            string description = string.Join("`", assignments.Select(x => x.Subject), " ", assignments.Select(x => x.Name), "`\n");
            var prefix = "!";

            var list = new SP1XEmbedBuilder()
                .WithTitle($"Assignments Due: ({assignments.Count()}")
                .WithDescription(description)
                .WithFooter($"Use `{prefix}<subject> <name>` to view an Assignment.")
                .WithStyle(EmbedStyle.Information)
                .Build();
            await this.Context.Channel.SendMessageAsync(embed: list);
        }

        [Command("assignment", RunMode = RunMode.Async)]
        public async Task AssignmentCmd(string subject, [Remainder] string argument)
        {
            var arguments = argument.Split(" ");

            if (subject != "create")
            {
                var assignment = await _DataAccessLayer.GetAssignment(subject, argument);
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

                var assignmentEmbed = new SP1XEmbedBuilder()
                    .WithTitle(assignment.Name)
                    .WithDescription(assignment.Content)
                    .WithFooter($"ID: {assignment.Id}")
                    .WithStyle(EmbedStyle.Information)
                    .Build();

                await Context.Channel.SendMessageAsync(embed: assignmentEmbed);
                return;
            }

            var socketGuildUser = Context.User as SocketGuildUser;

            switch (subject)
            {
                case "create":
                    var assignment = await _DataAccessLayer.GetAssignment(subject, arguments[0]);
                    if (assignment != null)
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Already Exists")
                            .WithDescription("There already exists an assignment in that subject, with that name.")
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

                    await _DataAccessLayer.CreateAssignment(subject, Context.User.Id,
                        arguments[0], string.Join(" ", arguments.Skip(1)));

                    var prefix = "!";

                    var created = new SP1XEmbedBuilder()
                        .WithTitle("Assignment Created!")
                        .WithDescription(
                            $"The assignment has been successfully created. You can view it by using `{prefix}assignment {arguments[0]}`")
                        .WithStyle(EmbedStyle.Success)
                        .Build();
                    await Context.Channel.SendMessageAsync(embed: created);
                    break;
            }
        }
    }
}
