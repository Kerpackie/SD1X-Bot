using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace Bot.Common
{
    public static class Extensions
    {

        public static bool IsPromoted(this SocketUser socketUser)
        {
            if (socketUser is not SocketGuildUser socketGuildUser)
            {
                return false;
            }

            try
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", false, true)
                    .Build();

                var studentRoleId = configuration.GetSection("Roles").GetValue<ulong>("Student");
                var studentRole = socketGuildUser.Guild.GetRole(studentRoleId);

                if (!socketGuildUser.Roles.Contains(studentRole) && !socketGuildUser.GuildPermissions.Administrator)
                {
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static async Task<IMessage> SendAssignmentAsync(this ITextChannel channel, string title,
            string description)
        {
            var embed = new SP1XEmbedBuilder()
                .WithDescription(description)
                .WithTitle(title)
                .WithStyle(EmbedStyle.Information)
                .WithFooter($"Assignment Sent: {DateTimeOffset.UtcNow.ToString()} UTC")
                .Build();

            var message = await channel.SendMessageAsync(embed: embed);
            return message;
        }
    }
}
