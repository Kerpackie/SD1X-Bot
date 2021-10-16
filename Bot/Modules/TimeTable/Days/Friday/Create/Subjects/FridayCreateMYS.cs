using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Friday.Create.Subjects
{
    public class FridayCreateMYS : TimeTableModuleBase
    {
        public FridayCreateMYS(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("FridayCreateMYS", RunMode = RunMode.Async)]
        [Alias("FriCreatemys")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateFridayMYSTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, Common.DaysOfWeek.Friday, Common.Subjects.MYS, location, time);
        }
    }
}
