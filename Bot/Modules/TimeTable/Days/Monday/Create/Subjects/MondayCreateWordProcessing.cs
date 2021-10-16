using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Monday.Create.Subjects
{
    public class MondayCreateWordProcessing : TimeTableModuleBase
    {
        public MondayCreateWordProcessing(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("MondayCreateWordProcessing", RunMode = RunMode.Async)]
        [Alias("MonCreateWP")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateMondayPPDTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, Common.DaysOfWeek.Monday, Common.Subjects.WordProcessing, location, time);
        }
    }
}