using Bot.Common;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Wednesday.Create.Subjects
{
    public class WednesdayCreateMaths : TimeTableModuleBase
    {
        public WednesdayCreateMaths(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("WednesdayCreateMaths", RunMode = RunMode.Async)]
        [Alias("WednesCreateMath", "WednesCreateMaths")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateWednesdayMathsTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, DaysOfWeek.Wednesday, Common.Subjects.MathForIT, location, time);
        }
    }
}
