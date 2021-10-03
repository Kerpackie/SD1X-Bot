using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bot.Common
{
    public class RequirePromoted : PreconditionAttribute
    {
        public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            if (context.User is SocketGuildUser user)
            {
                var studentRoleId = services.GetRequiredService<IConfiguration>().GetSection("Roles")
                    .GetValue<ulong>("Student");

                var studentRole = context.Guild.GetRole(studentRoleId);

                if (user.Roles.Contains(studentRole))
                {
                    return Task.FromResult(PreconditionResult.FromSuccess());
                }
                else
                {
                    return Task.FromResult(PreconditionResult.FromError("User is not Authorized."));
                }
            }
            else
            {
                return Task.FromResult(PreconditionResult.FromError("Command was run outside of guild."));
            }
        }
    }
}
