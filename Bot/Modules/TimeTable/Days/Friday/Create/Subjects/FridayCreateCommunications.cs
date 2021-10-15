using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Friday.Create.Subjects
{
    public class FridayCreateCommunications : TimeTableModuleBase
    {
        public FridayCreateCommunications(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("FridayCreateCommunications", RunMode = RunMode.Async)]
        [Alias("Fricreatecomms")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateFridayCommunicationsTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, Common.DaysOfWeek.Friday, Common.Subjects.Communications, location, time);
        }
    }
}
