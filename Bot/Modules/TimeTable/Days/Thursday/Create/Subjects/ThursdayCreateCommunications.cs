using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Thursday.Create.Subjects
{
    public class ThursdayCreateCommunications : TimeTableModuleBase
    {
        public ThursdayCreateCommunications(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("ThursdayCreateCommunications", RunMode = RunMode.Async)]
        [Alias("Thurscreatecomms")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateThursdayCommunicationsTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, Common.DaysOfWeek.Thursday, Common.Subjects.Communications, location, time);
        }
    }
}
