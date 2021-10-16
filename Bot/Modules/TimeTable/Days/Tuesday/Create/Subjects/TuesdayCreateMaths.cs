using Bot.Common;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Tuesday.Create.Subjects
{
    public class TuesdayCreateMaths : TimeTableModuleBase
    {
        public TuesdayCreateMaths(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("TuesdayCreateMaths", RunMode = RunMode.Async)]
        [Alias("TuesCreateMath", "TuesCreateMaths")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateTuesdayMathsTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, DaysOfWeek.Tuesday, Common.Subjects.MathForIT, location, time);
        }
    }
}
