using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Wednesday.Create.Subjects
{
    public class WednesdayCreateSoftwareArchitecture : TimeTableModuleBase
    {
        public WednesdayCreateSoftwareArchitecture(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("WednesdayCreateSoftwareArchitecture", RunMode = RunMode.Async)]
        [Alias("WednesCreateSA")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateWednesdaySoftwareArchitectureTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, Common.DaysOfWeek.Wednesday, Common.Subjects.SoftwareArchitecture, location, time);
        }
    }
}
