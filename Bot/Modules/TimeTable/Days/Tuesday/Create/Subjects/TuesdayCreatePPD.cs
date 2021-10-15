using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Tuesday.Create.Subjects
{
    public class TuesdayCreatePPD : TimeTableModuleBase
    {
        public TuesdayCreatePPD(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("TuesdayCreatePPD", RunMode = RunMode.Async)]
        [Alias("TuescreatePPD")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateTuesdayPPDTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, Common.DaysOfWeek.Tuesday, Common.Subjects.PPD, location, time);
        }
    }
}
