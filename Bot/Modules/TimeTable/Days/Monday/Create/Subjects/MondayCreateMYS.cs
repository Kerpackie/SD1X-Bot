using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Monday.Create.Subjects
{
    public class MondayCreateMYS : TimeTableModuleBase
    {
        public MondayCreateMYS(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("MondayCreateMYS", RunMode = RunMode.Async)]
        [Alias("Moncreatemys")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateMondayMYSTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, Common.DaysOfWeek.Monday, Common.Subjects.MYS, location, time);
        }
    }
}
