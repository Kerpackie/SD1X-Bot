using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Monday.Create.Subjects
{
    public class MondayCreateSoftwareArchitecture : TimeTableModuleBase
    {
        public MondayCreateSoftwareArchitecture(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("MondayCreateSoftwareArchitecture", RunMode = RunMode.Async)]
        [Alias("MonCreateSA")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateMondaySoftwareArchitectureTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, Common.DaysOfWeek.Monday, Common.Subjects.SoftwareArchitecture, location, time);
        }
    }
}
