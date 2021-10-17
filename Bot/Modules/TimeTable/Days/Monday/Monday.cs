using Bot.Common;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Monday
{
    public class Monday : TimeTableModuleBase
    {
        public Monday(IServiceProvider serviceProvider, IConfiguration configuration) : base (serviceProvider, configuration)
        {
        }

        [Command("Monday", RunMode = RunMode.Async)]
        [Alias("Mon")]
        [RequireUserPermission(Discord.GuildPermission.SendMessages)]

        public async Task GetMondayTimeTable()
        {
            await _TimeTableManager.PostDailyTimeTableEmbed(Context.Guild, Context.Channel.Id, DaysOfWeek.Monday);
        }
    }
}
