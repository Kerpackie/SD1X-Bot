using System;
using System.Linq;
using System.Threading.Tasks;
using Bot.Common;
using Discord;
using Discord.Commands;
using Microsoft.Extensions.Configuration;

namespace Bot.Modules.Subjects
{
    public class AssignmentId : SP1XModuleBase
    {
        public AssignmentId(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("assignmentids", RunMode = RunMode.Async)]
        [RequireUserPermission(GuildPermission.Administrator)]

        public async Task GetAssignmentIds()
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
                .WithTitle($"Assignments Due: {assignments.Count()}");

            foreach (var assignment in assignments)
            {
                var assignmentEmbedField = new EmbedFieldBuilder()
                    .WithName($"{assignment.Subject}")
                    .WithValue($"`{assignment.Subject} {assignment.Name} Id: {assignment.Id}`")
                    .WithIsInline(true);

                assignmentEmbedBuilder.AddField(assignmentEmbedField);
            }

            await Context.Channel.SendMessageAsync(null, false, assignmentEmbedBuilder.Build());
        }

    }
}
