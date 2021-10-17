using Bot.Common;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Tuesday
{
    public class Tuesday : TimeTableModuleBase
    {
        public Tuesday(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("tuesday", RunMode = RunMode.Async)]
        [Alias("tues")]
        [RequireUserPermission(Discord.GuildPermission.SendMessages)]

        public async Task GetTuesdayTimeTable()
        {
            await _TimeTableManager.PostDailyTimeTableEmbed(Context.Guild, Context.Channel.Id, DaysOfWeek.Tuesday);
        }
    }
}
