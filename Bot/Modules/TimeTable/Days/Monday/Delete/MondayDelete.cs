using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Monday.Delete
{
    public class MondayDelete : TimeTableModuleBase
    {
        public MondayDelete(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("MondayDelete", RunMode = RunMode.Async)]
        [Alias("MonDelete", "MonDel")]
        [RequireUserPermission(Discord.GuildPermission.SendMessages)]

        public async Task DeleteMondayTimeTable(string time)
        {
            await _TimeTableManager.DeleteTimeTableEntry(Context.Guild, Context.Channel.Id, Common.DaysOfWeek.Monday, time);
        }
    }
}
