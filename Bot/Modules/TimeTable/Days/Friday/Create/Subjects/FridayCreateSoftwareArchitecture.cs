using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Friday.Create.Subjects
{
    public class FridayCreateSoftwareArchitecture : TimeTableModuleBase
    {
        public FridayCreateSoftwareArchitecture(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("FridayCreateSoftwareArchitecture", RunMode = RunMode.Async)]
        [Alias("FriCreateSA")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateFridaySoftwareArchitectureTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, Common.DaysOfWeek.Friday, Common.Subjects.SoftwareArchitecture, location, time);
        }
    }
}
