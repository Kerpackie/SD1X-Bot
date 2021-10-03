using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Server
    {
        /// <summary>
        /// Gets or Sets the ID of the Server.
        /// </summary>
        public ulong Id { get; set; }

        /// <summary>
        /// Gets or Sets the prefix for bot to execute commands.
        /// </summary>
        public string Prefix { get; set; }

    }
}
