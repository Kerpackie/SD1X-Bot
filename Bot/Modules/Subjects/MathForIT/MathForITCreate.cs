using System;
using System.Linq;
using System.Threading.Tasks;
using Bot.Common;
using Discord;
using Discord.Commands;
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
        [RequireUserPermission(GuildPermission.SendMessages)]

        public async Task MathCreateCmd([Remainder] string argument)
        {
            var arguments = argument.Split(" ");
            var subject = Common.Subjects.MathForIT;

            var mathAssignment = await _DataAccessLayer.GetAssignment(subject, arguments[0]);
            if (mathAssignment != null)
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
                    $"The Assignment has been successfully created. You can view it by using `{prefix}math {arguments[0]}`")
                .WithStyle(EmbedStyle.Success)
                .Build();
            await Context.Channel.SendMessageAsync(embed: created);
        }
    }
}
