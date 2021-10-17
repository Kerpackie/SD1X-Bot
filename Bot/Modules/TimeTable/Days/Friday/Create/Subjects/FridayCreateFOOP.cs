using Bot.Common;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Friday.Create.Subjects
{
    public class FridayCreateFOOP : TimeTableModuleBase
    {
        public FridayCreateFOOP(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("FridayCreateFOOP", RunMode = RunMode.Async)]
        [Alias("FriCreateFOOP")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateFridayFOOPTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, DaysOfWeek.Friday, Common.Subjects.FOOP, location, time);
        }
    }
}
