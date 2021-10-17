using Bot.Common;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Friday
{
    public class Friday : TimeTableModuleBase
    {
        public Friday(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("Friday", RunMode = RunMode.Async)]
        [Alias("Fri")]
        [RequireUserPermission(Discord.GuildPermission.SendMessages)]

        public async Task GetFridayTimeTable()
        {
            await _TimeTableManager.PostDailyTimeTableEmbed(Context.Guild, Context.Channel.Id, DaysOfWeek.Friday);
        }
    }
}
