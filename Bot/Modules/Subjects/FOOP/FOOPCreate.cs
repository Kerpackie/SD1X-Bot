using Bot.Common;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace Bot.Modules.Subjects.FOOP
{
    public class FOOPCreate : SP1XModuleBase
    {
        public FOOPCreate(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider,
            configuration)
        {
        }

        [Command("foopcreate", RunMode = RunMode.Async)]
        [Alias("oopcreate")]
        [RequireUserPermission(GuildPermission.SendMessages)]

        public async Task FOOPCmd([Remainder] string argument)
        {
            var arguments = argument.Split(" ");
            var subject = Common.Subjects.FOOP;

            var foopAssignment = await _DataAccessLayer.GetAssignment(subject, arguments[0]);
            if (foopAssignment != null)
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

            var prefix = await _DataAccessLayer.GetGuildPrefix(Context.Guild.Id);

            var created = new SP1XEmbedBuilder()
                .WithTitle("FOOP Assignment Created!")
                .WithDescription($"The Assignment has been successfully created. You can view it by using `{prefix}foop {arguments[0]}`")
                .WithStyle(EmbedStyle.Success)
                .Build();
            await Context.Channel.SendMessageAsync(embed: created);
                
        }
    }
}
