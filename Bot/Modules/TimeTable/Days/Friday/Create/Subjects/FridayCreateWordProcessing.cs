using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Friday.Create.Subjects
{
    public class FridayCreateWordProcessing : TimeTableModuleBase
    {
        public FridayCreateWordProcessing(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("FridayCreateWordProcessing", RunMode = RunMode.Async)]
        [Alias("FriCreateWP")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateFridayPPDTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, Common.DaysOfWeek.Friday, Common.Subjects.WordProcessing, location, time);
        }
    }
}
