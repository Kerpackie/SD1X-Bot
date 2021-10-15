using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Wednesday.Create.Subjects
{
    public class WednesdayCreateMYS : TimeTableModuleBase
    {
        public WednesdayCreateMYS(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("WednesdayCreateMYS", RunMode = RunMode.Async)]
        [Alias("Wednescreatemys")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateWednesdayMYSTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, Common.DaysOfWeek.Wednesday, Common.Subjects.MYS, location, time);
        }
    }
}
