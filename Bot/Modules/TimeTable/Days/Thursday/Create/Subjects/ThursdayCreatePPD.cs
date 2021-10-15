using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Thursday.Create.Subjects
{
    public class ThursdayCreatePPD : TimeTableModuleBase
    {
        public ThursdayCreatePPD(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("ThursdayCreatePPD", RunMode = RunMode.Async)]
        [Alias("ThurscreatePPD")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateThursdayPPDTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, Common.DaysOfWeek.Thursday, Common.Subjects.PPD, location, time);
        }
    }
}
