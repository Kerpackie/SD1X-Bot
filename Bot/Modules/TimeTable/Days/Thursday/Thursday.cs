using Bot.Common;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Thursday
{
    public class Thursday : TimeTableModuleBase
    {
        public Thursday(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("Thursday", RunMode = RunMode.Async)]
        [Alias("thurs")]
        [RequireUserPermission(Discord.GuildPermission.SendMessages)]

        public async Task GetThursdayTimeTable()
        {
            await _TimeTableManager.PostDailyTimeTableEmbed(Context.Guild, Context.Channel.Id, DaysOfWeek.Thursday);
        }
    }
}
