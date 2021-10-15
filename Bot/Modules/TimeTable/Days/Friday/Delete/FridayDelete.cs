using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Modules.TimeTable.Days.Friday.Delete
{
    public class FridayDelete : TimeTableModuleBase
    {
        public FridayDelete(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("FridayDelete", RunMode = RunMode.Async)]
        [Alias("FriDelete", "FriDel")]
        [RequireUserPermission(Discord.GuildPermission.SendMessages)]

        public async Task DeleteFridayTimeTable(string time)
        {
            await _TimeTableManager.DeleteTimeTableEntry(Context.Guild, Context.Channel.Id, Common.DaysOfWeek.Friday, time);
        }
    }
}
