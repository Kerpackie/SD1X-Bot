using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Tuesday.Delete
{
    public class TuesdayDelete : TimeTableModuleBase
    {
        public TuesdayDelete(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("TuesdayDelete", RunMode = RunMode.Async)]
        [Alias("TuesDelete", "TuesDel")]
        [RequireUserPermission(Discord.GuildPermission.SendMessages)]

        public async Task DeleteTuesdayTimeTable(string time)
        {
            await _TimeTableManager.DeleteTimeTableEntry(Context.Guild, Context.Channel.Id, Common.DaysOfWeek.Tuesday, time);
        }
    }
}
