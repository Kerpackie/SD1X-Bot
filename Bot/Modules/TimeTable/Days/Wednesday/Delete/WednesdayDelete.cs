using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Wednesday.Delete
{
    public class WednesdayDelete : TimeTableModuleBase
    {
        public WednesdayDelete(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("WednesdayDelete", RunMode = RunMode.Async)]
        [Alias("WedsDelete", "WedDel", "WedsDel")]
        [RequireUserPermission(Discord.GuildPermission.SendMessages)]

        public async Task DeleteWednesdayTimeTable(string time)
        {
            await _TimeTableManager.DeleteTimeTableEntry(Context.Guild, Context.Channel.Id, Common.DaysOfWeek.Wednesday, time);
        }
    }
}
