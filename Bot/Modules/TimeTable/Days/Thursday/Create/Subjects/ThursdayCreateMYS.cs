using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Thursday.Create.Subjects
{
    public class ThursdayCreateMYS : TimeTableModuleBase
    {
        public ThursdayCreateMYS(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("ThursdayCreateMYS", RunMode = RunMode.Async)]
        [Alias("Thurscreatemys")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateThursdayMYSTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, Common.DaysOfWeek.Thursday, Common.Subjects.MYS, location, time);
        }
    }
}
