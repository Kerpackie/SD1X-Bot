using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class HTMLTagModel
    {
        public int Id { get; set; }
        public string Tag { get; set; }
        public string Content { get; set; }
        public ulong OwnerId { get; set; }
    }
}
