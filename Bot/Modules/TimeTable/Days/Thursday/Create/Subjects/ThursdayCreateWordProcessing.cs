using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Thursday.Create.Subjects
{
    public class ThursdayCreateWordProcessing : TimeTableModuleBase
    {
        public ThursdayCreateWordProcessing(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("ThursdayCreateWordProcessing", RunMode = RunMode.Async)]
        [Alias("ThursCreateWP")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateThursdayPPDTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, Common.DaysOfWeek.Thursday, Common.Subjects.WordProcessing, location, time);
        }
    }
}
