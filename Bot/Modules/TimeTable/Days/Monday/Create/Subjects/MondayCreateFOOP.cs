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
    public class MondayCreateFOOP : TimeTableModuleBase
    {
        public MondayCreateFOOP(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("MondayCreateFOOP", RunMode = RunMode.Async)]
        [Alias("MonCreateFOOP")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateMondayFOOPTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, DaysOfWeek.Monday, Common.Subjects.FOOP, location, time);
        }
    }
}
