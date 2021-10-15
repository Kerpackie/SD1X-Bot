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
    public class ThursdayCreateFOOP : TimeTableModuleBase
    {
        public ThursdayCreateFOOP(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("ThursdayCreateFOOP", RunMode = RunMode.Async)]
        [Alias("ThursCreateFOOP")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateThursdayFOOPTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, DaysOfWeek.Thursday, Common.Subjects.FOOP, location, time);
        }
    }
}
