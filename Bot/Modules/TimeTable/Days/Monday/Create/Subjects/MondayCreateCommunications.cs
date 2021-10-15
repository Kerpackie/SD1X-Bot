using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Monday.Create.Subjects
{
    public class ThursdayCreateCommunications : TimeTableModuleBase
    {
        public ThursdayCreateCommunications(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("MondayCreateCommunications", RunMode = RunMode.Async)]
        [Alias("Moncreatecomms")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateMondayCommunicationsTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, Common.DaysOfWeek.Monday, Common.Subjects.Communications, location, time);
        }
    }
}
