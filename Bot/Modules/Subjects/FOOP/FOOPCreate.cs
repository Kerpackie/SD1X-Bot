using Bot.Common;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Modules.Subjects.FOOP
{
    class FOOPCreate : SP1XModuleBase
    {
        public FOOPCreate(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider,
            configuration)
        {
        }

        [Command("foopcreate", RunMode = RunMode.Async)]
        [Alias("oopcreate")]

        public async Task FOOPCmd(string name, [Remainder] string argument)
        {
            var arguments = argument.Split(" ");
            var subject = "FOOP";

            var socketGuildUser = Context.User as SocketGuildUser;

            var foopAssignment = await _DataAccessLayer.GetAssignment(subject, name);
            if (foopAssignment == null)
            {
                var embed = new SP1XEmbedBuilder()
                    .WithTitle("Not Found")
                    .WithDescription("The assignment you requested could not be found.")
                    .WithStyle(EmbedStyle.Error)
                    .Build();

                await Context.Channel.SendMessageAsync(embed: embed);
                return;
            }
            if (foopAssignment.OwnerId != Context.User.Id && !socketGuildUser.GuildPermissions.Administrator)
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

            var prefix = await _DataAccessLayer.GetGuildPrefix(Context.Guild.Id);

            var created = new SP1XEmbedBuilder()
                .WithTitle("FOOP Assignment Created!")
                .WithDescription($"The Assignment has been successfully created. You can view it by using `{prefix}foop {arguments[1]}`")
                .WithStyle(EmbedStyle.Success)
                .Build();
            await Context.Channel.SendMessageAsync(embed: created);
                
        }
    }
}
