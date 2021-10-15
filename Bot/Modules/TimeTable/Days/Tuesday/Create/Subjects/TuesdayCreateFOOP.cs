using Bot.Common;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Tuesday.Create.Subjects
{
    public class TuesdayCreateFOOP : TimeTableModuleBase
    {
        public TuesdayCreateFOOP(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("TuesdayCreateFOOP", RunMode = RunMode.Async)]
        [Alias("TuesCreateFOOP")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateTuesdayFOOPTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, DaysOfWeek.Tuesday, Common.Subjects.FOOP, location, time);
        }
    }
}
