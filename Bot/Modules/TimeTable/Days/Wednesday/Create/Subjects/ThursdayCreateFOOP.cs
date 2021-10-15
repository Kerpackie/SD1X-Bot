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
    public class WednesdayCreateFOOP : TimeTableModuleBase
    {
        public WednesdayCreateFOOP(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("WednesdayCreateFOOP", RunMode = RunMode.Async)]
        [Alias("WednesCreateFOOP")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateWednesdayFOOPTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, DaysOfWeek.Wednesday, Common.Subjects.FOOP, location, time);
        }
    }
}
