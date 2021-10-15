using Bot.Common;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Monday.Create.Subjects
{
    public class MondayCreateMaths : TimeTableModuleBase
    {
        public MondayCreateMaths(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("MondayCreateMaths", RunMode = RunMode.Async)]
        [Alias("MonCreateMath", "MonCreateMaths")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateMondayMathsTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, DaysOfWeek.Monday, Common.Subjects.MathForIT, location, time);
        }
    }
}
