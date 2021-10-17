using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Monday.Create.Subjects
{
    public class MondayCreateWebAuthoring : TimeTableModuleBase
    {
        public MondayCreateWebAuthoring(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("MondayCreateWebAuthoring", RunMode = RunMode.Async)]
        [Alias("MonCreateWebAuth")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateMondayWebAuthoringTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, Common.DaysOfWeek.Monday, Common.Subjects.WebAuthoring, location, time);
        }
    }
}
