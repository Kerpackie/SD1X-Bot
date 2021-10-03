using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Data;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bot.Modules
{
    public abstract class SP1XModuleBase : ModuleBase<SocketCommandContext>
    {
        public readonly DataAccessLayer _DataAccessLayer;
        public readonly IConfiguration _Configuration;
        public readonly IServiceScope _Scope;

        protected SP1XModuleBase(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _Scope = serviceProvider.CreateScope();
            _DataAccessLayer = _Scope.ServiceProvider.GetRequiredService<DataAccessLayer>();
            _Configuration = configuration;
        }
    }
}
