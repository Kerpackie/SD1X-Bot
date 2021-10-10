using System;
using System.Linq;
using System.Threading.Tasks;
using Bot.Common;
using Discord;
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
        public async Task GetAssignments()
        {
            var assignments = await _DataAccessLayer.GetAssignments();

            if (assignments == null)
            {
                var noAssignments = new SP1XEmbedBuilder()
                    .WithTitle("No Assignments Found")
                    .WithDescription("This server does not have any assignments yet.")
                    .WithStyle(EmbedStyle.Error)
                    .Build();

                await Context.Channel.SendMessageAsync(embed: noAssignments);
                return;
            }

            var assignmentEmbedBuilder = new EmbedBuilder()
                .WithColor(Colors.Information)
                .WithTitle($"Assignments Due: {assignments.Count()}")
                .WithDescription("Run the command `!<subject> <name>` to get information on an assignment");
            foreach (var assignment in assignments)
            {
                var assignmentEmbedField = new EmbedFieldBuilder()
                    .WithName($"{assignment.Subject}")
                    .WithValue($"{assignment.Name} \n `ID: {assignment.Id}`")
                    .WithIsInline(true);

                assignmentEmbedBuilder.AddField(assignmentEmbedField);
            }

            await Context.Channel.SendMessageAsync(null, false, assignmentEmbedBuilder.Build());
        }

        [Command("assignment", RunMode = RunMode.Async)]
        public async Task AssignmentCmd(string subject, string name)
        {

            var assignment = await _DataAccessLayer.GetAssignment(subject, name);
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

        }
    }
}
