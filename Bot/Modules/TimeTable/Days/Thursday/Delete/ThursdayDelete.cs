using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Thursday.Delete
{
    public class ThursdayDelete : TimeTableModuleBase
    {
        public ThursdayDelete(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("ThursdayDelete", RunMode = RunMode.Async)]
        [Alias("ThursDelete", "ThursDel")]
        [RequireUserPermission(Discord.GuildPermission.SendMessages)]

        public async Task DeleteThursdayTimeTable(string time)
        {
            await _TimeTableManager.DeleteTimeTableEntry(Context.Guild, Context.Channel.Id, Common.DaysOfWeek.Thursday, time);
        }
    }
}
