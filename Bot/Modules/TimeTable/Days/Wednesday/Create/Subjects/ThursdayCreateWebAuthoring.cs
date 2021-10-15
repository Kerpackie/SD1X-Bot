using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Wednesday.Create.Subjects
{
    public class WednesdayCreateWebAuthoring : TimeTableModuleBase
    {
        public WednesdayCreateWebAuthoring(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("WednesdayCreateWebAuthoring", RunMode = RunMode.Async)]
        [Alias("WednesCreateWebAuth")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateWednesdayWebAuthoringTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, Common.DaysOfWeek.Wednesday, Common.Subjects.WebAuthoring, location, time);
        }
    }
}
