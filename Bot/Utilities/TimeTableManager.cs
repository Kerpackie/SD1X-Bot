using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Utilities
{
    public class TimeTableManager
    {
        private readonly DataAccessLayer _dataAccessLayer;

        public TimeTableManager(DataAccessLayer dataAccessLayer)
        {
            _dataAccessLayer = dataAccessLayer;
        }

    }
}
