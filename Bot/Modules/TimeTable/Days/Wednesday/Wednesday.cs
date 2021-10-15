using Bot.Common;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Wednesday
{
    public class Wednesday : TimeTableModuleBase
    {
        public Wednesday(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("Wednesday", RunMode = RunMode.Async)]
        [Alias("Wed", "Weds")]
        [RequireUserPermission(Discord.GuildPermission.SendMessages)]

        public async Task GetWednesdayTimeTable()
        {
            await _TimeTableManager.PostDailyTimeTableEmbed(Context.Guild, Context.Channel.Id, DaysOfWeek.Wednesday);
        }
    }
}
