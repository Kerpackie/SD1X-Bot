using Bot.Utilities;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable
{
    public class TimeTable : TimeTableModuleBase
    {
        public TimeTable(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("timetable", RunMode = RunMode.Async)]
        [Alias("tt")]
        [RequireUserPermission(Discord.GuildPermission.SendMessages)]

        public async Task GetWeeklyTimeTable()
        {
            await _TimeTableManager.PostWeeklyTimeTableEmbed(Context.Guild, Context.Channel.Id);
        }
    }
}
