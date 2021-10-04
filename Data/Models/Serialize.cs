using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Data.Models
{
    public static class Serialize
    {
        public static string ToJson(this Meme self)
        {
            return JsonConvert.SerializeObject(self, Converter.Settings);
        }
    }
}
