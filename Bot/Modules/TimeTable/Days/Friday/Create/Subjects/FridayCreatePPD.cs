using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Friday.Create.Subjects
{
    public class FridayCreatePPD : TimeTableModuleBase
    {
        public FridayCreatePPD(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("FridayCreatePPD", RunMode = RunMode.Async)]
        [Alias("FricreatePPD")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateFridayPPDTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, Common.DaysOfWeek.Friday, Common.Subjects.PPD, location, time);
        }
    }
}
