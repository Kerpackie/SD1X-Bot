using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Discord.Commands;
using Microsoft.Extensions.Configuration;

namespace Bot.Modules
{
    public class ConfigModule : ModuleBase<SocketCommandContext>
    {
        private readonly DataAccessLayer dataAccessLayer;

        public ConfigModule(DataAccessLayer dataAccessLayer)
        {
            this.dataAccessLayer = dataAccessLayer;
        }

        [Command("prefix", RunMode = RunMode.Async)]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]
        public async Task Prefix(string prefix = null)
        {
            if (prefix == null)
            {
                var guildPrefix = await this.dataAccessLayer.GetGuildPrefix(this.Context.Guild.Id) ?? "!";
                await this.ReplyAsync($"The current prefix of this bot is `{guildPrefix}`.");
                return;
            }

            if (prefix.Length > 8)
            {
                await this.ReplyAsync("The length of the new prefix is too long!");
                return;
            }

            await this.dataAccessLayer.ModifyGuildPrefix(this.Context.Guild.Id, prefix);
            await this.ReplyAsync($"The prefix has been adjusted to `{prefix}`.");
        }
    }
}
