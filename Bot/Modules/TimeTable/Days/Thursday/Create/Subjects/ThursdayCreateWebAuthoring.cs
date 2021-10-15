using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Thursday.Create.Subjects
{
    public class ThursdayCreateWebAuthoring : TimeTableModuleBase
    {
        public ThursdayCreateWebAuthoring(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("ThursdayCreateWebAuthoring", RunMode = RunMode.Async)]
        [Alias("ThursCreateWebAuth")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateThursdayWebAuthoringTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, Common.DaysOfWeek.Thursday, Common.Subjects.WebAuthoring, location, time);
        }
    }
}
