using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Tuesday.Create.Subjects
{
    public class TuesdayCreateWebAuthoring : TimeTableModuleBase
    {
        public TuesdayCreateWebAuthoring(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("TuesdayCreateWebAuthoring", RunMode = RunMode.Async)]
        [Alias("TuesCreateWebAuth")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateTuesdayWebAuthoringTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, Common.DaysOfWeek.Tuesday, Common.Subjects.WebAuthoring, location, time);
        }
    }
}
