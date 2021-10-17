using System.Threading.Tasks;
using Data;
using Discord.Commands;

namespace Bot.Modules.Config
{
    public class Prefix : ModuleBase<SocketCommandContext>
    {
        private readonly DataAccessLayer _dataAccessLayer;

        public Prefix(DataAccessLayer dataAccessLayer)
        {
            _dataAccessLayer = dataAccessLayer;
        }

        [Command("prefix", RunMode = RunMode.Async)]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]
        public async Task PrefixCmd(string prefix = null)
        {
            if (prefix == null)
            {
                var guildPrefix = await _dataAccessLayer.GetGuildPrefix(Context.Guild.Id) ?? "!";
                await this.ReplyAsync($"The current prefix of this bot is `{guildPrefix}`.");
                return;
            }

            if (prefix.Length > 8)
            {
                await ReplyAsync("The length of the new prefix is too long!");
                return;
            }

            await _dataAccessLayer.ModifyGuildPrefix(Context.Guild.Id, prefix);
            await ReplyAsync($"The prefix has been adjusted to `{prefix}`.");
        }
    }
}
