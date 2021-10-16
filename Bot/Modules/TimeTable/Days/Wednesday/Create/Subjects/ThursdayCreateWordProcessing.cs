using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Wednesday.Create.Subjects
{
    public class WednesdayCreateWordProcessing : TimeTableModuleBase
    {
        public WednesdayCreateWordProcessing(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("WednesdayCreateWordProcessing", RunMode = RunMode.Async)]
        [Alias("WednesCreateWP")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateWednesdayPPDTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, Common.DaysOfWeek.Wednesday, Common.Subjects.WordProcessing, location, time);
        }
    }
}
