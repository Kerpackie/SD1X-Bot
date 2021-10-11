using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bot.Common;
using Data;
using Discord;

namespace Bot.Utilities
{
    public class AssignmentControl
    {
        private readonly DataAccessLayer _dataAccessLayer;

        public AssignmentControl(DataAccessLayer dataAccessLayer)
        {
            _dataAccessLayer = dataAccessLayer;
        }

        public async Task SendAssignmentAsync(IGuild guild, string title, string description)
        {
            var channelIds = await _dataAccessLayer.GetAssignmentChannels(guild.Id);

            if (channelIds == null)
            {
                return;
            }
            foreach (var channelId in channelIds)
            {
                var fetchedChannel = await guild.GetTextChannelAsync(channelId.Channel);
                await fetchedChannel.SendAssignmentAsync(title, description);
            }
        }
    }
}
