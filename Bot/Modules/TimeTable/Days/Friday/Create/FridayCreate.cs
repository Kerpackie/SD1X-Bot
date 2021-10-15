using Bot.Common;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Friday.Create
{
    public class FridayCreate : TimeTableModuleBase
    {
        public FridayCreate(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("Fridaycreate", RunMode = RunMode.Async)]
        [Alias("Fricreate")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]

        public async Task CreateFridayTimeTable(string time, string location, [Remainder] string subject)
        {
            await _TimeTableManager.CreateNewTimeTableEntry(Context.Guild, Context.Channel.Id, DaysOfWeek.Friday, subject, location, time);
        }
    }
}
