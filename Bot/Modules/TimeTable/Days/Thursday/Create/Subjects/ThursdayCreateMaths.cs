using Bot.Common;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Thursday.Create.Subjects
{
    public class ThursdayCreateMaths : TimeTableModuleBase
    {
        public ThursdayCreateMaths(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("ThursdayCreateMaths", RunMode = RunMode.Async)]
        [Alias("ThursCreateMath", "ThursCreateMaths")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateThursdayMathsTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, DaysOfWeek.Thursday, Common.Subjects.MathForIT, location, time);
        }
    }
}
