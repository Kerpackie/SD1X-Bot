using Bot.Common;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Friday.Create.Subjects
{
    public class FridayCreateMaths : TimeTableModuleBase
    {
        public FridayCreateMaths(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("FridayCreateMaths", RunMode = RunMode.Async)]
        [Alias("FriCreateMath", "FriCreateMaths")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateFridayMathsTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, DaysOfWeek.Friday, Common.Subjects.MathForIT, location, time);
        }
    }
}
