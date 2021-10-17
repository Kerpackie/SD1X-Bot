using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Monday.Create.Subjects
{
    public class MondayCreatePPD : TimeTableModuleBase
    {
        public MondayCreatePPD(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("MondayCreatePPD", RunMode = RunMode.Async)]
        [Alias("MoncreatePPD")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateMondayPPDTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, Common.DaysOfWeek.Monday, Common.Subjects.PPD, location, time);
        }
    }
}
