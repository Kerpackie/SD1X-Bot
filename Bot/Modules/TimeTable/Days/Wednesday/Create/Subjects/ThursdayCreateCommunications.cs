using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Wednesday.Create.Subjects
{
    public class WednesdayCreateCommunications : TimeTableModuleBase
    {
        public WednesdayCreateCommunications(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("WednesdayCreateCommunications", RunMode = RunMode.Async)]
        [Alias("Wednescreatecomms")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateWednesdayCommunicationsTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, Common.DaysOfWeek.Wednesday, Common.Subjects.Communications, location, time);
        }
    }
}
