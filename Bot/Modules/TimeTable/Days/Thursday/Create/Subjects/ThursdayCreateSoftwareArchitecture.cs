using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Thursday.Create.Subjects
{
    public class ThursdayCreateSoftwareArchitecture : TimeTableModuleBase
    {
        public ThursdayCreateSoftwareArchitecture(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("ThursdayCreateSoftwareArchitecture", RunMode = RunMode.Async)]
        [Alias("ThursCreateSA")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateThursdaySoftwareArchitectureTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, Common.DaysOfWeek.Thursday, Common.Subjects.SoftwareArchitecture, location, time);
        }
    }
}
