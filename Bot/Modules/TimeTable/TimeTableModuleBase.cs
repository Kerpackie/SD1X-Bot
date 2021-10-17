using Bot.Utilities;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Bot.Modules.TimeTable
{
    public abstract class TimeTableModuleBase : ModuleBase<SocketCommandContext>
    {
        public readonly TimeTableManager _TimeTableManager;
        public readonly IConfiguration _Configuration;
        public readonly IServiceScope _Scope;

        protected TimeTableModuleBase(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _Scope = serviceProvider.CreateScope();
            _TimeTableManager = _Scope.ServiceProvider.GetRequiredService<TimeTableManager>();
            _Configuration = configuration;
        }
    }
}
