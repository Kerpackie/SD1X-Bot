using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Tuesday.Create.Subjects
{
    public class TuesdayCreateSoftwareArchitecture : TimeTableModuleBase
    {
        public TuesdayCreateSoftwareArchitecture(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("TuesdayCreateSoftwareArchitecture", RunMode = RunMode.Async)]
        [Alias("TuesCreateSA")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateTuesdaySoftwareArchitectureTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, Common.DaysOfWeek.Tuesday, Common.Subjects.SoftwareArchitecture, location, time);
        }
    }
}
