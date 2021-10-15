using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Tuesday.Create.Subjects
{
    public class TuesdayCreateCommunications : TimeTableModuleBase
    {
        public TuesdayCreateCommunications(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("TuesdayCreateCommunications", RunMode = RunMode.Async)]
        [Alias("Tuescreatecomms")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateTuesdayCommunicationsTimeTable(string time, string location)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, Common.DaysOfWeek.Tuesday, Common.Subjects.Communications, location, time);
        }
    }
}
